// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.Xml;
using BBCFReplayConsole;
using BBCFReplayLib;
using CommandLine;

class Program
{
    // Verb definitions //////////////////////////////////////////////////////////////////////
    public class GlobalOptions
    {
        [Option('v', "verbose", Default = false, HelpText = "Output should be more verbose")]
        public bool Verbose { get; set; }

        [Option('f', "files", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }
        // look up how to provide a list of files without a switch
    }

    [Verb("json")]
    public class JsonOptions : GlobalOptions
    {
        [Option('o', "output", Required = true, HelpText = "Folder to save JSON file(s) in.")]
        public string Output { get; set; }
    }

    [Verb("edit")]
    public class EditOptions : GlobalOptions
    {
    }

    [Verb("rename")]
    public class RenameOptions: GlobalOptions
    {
        [Option('o', "output", Required = true, HelpText = "Folder to save renamed file(s) in.")]
        public string Output { get; set; }

        [Option('m', "format", Required = true, HelpText = "Formatting string to name files according to. Please see -help for more information.")]
        public string Format { get; set; }
    }

    // Main func //////////////////////////////////////////////////////////////////////
    public static int Main(string[] args)
    {

        var exitCode = Parser.Default.ParseArguments<EditOptions, JsonOptions, RenameOptions>(args)
            .MapResult(
                (EditOptions opts) => RunEdit(opts),
                (JsonOptions opts) => RunJson(opts),
                (RenameOptions opts) => RunRename(opts),
                HandleErrors);

        return exitCode;
    }

    // Verb funcs //////////////////////////////////////////////////////////////////////
    private static int RunEdit(EditOptions options)
    {
        return 0;
    }

    private static int RunRename(RenameOptions options)
    {
        var variableNames = ReplayRenamingTools.GetVariables(options.Format);
        List<string> datFilePaths = GetDatFiles(options.InputFiles);

        Console.WriteLine($"Found {datFilePaths.Count} data files -- continue? [y/N]");
        var toContinue = Console.ReadLine();
        if(toContinue != "y") { return 0; }
        Console.WriteLine("Continuing...");

        foreach (string datFilePath in datFilePaths)
        {
            var rh = ReadDatFile(datFilePath);
            var varMaps = ReplayRenamingTools.MapVariableValues(variableNames, rh);
            var newName = ReplayRenamingTools.CreateNewName(options.Format, varMaps);
            var response = CheckOutputPath(options.Output);
            if (response != 0) 
            {
                Console.WriteLine("ERR: Encountered an error while trying to rename files.");
                return response; 
            }
            var path = GetNewDatPath(options.Output, newName);
            CopyDatFile(path, datFilePath, rh);
        }

        Console.WriteLine($"Done. {datFilePaths.Count} files written to {options.Output}. Exiting.");
        return 0;
    }

    private static void CopyDatFile(string path, string datFilePath, ReplayHeader rh)
    {
        // todo: add option to overwrite, add other checks
        File.Copy(datFilePath, path, true);
        // make this toggleable or smth
        File.SetCreationTime(path, rh.Date1);
    }

    private static int RunJson(JsonOptions options)
    {
        List<string> datFiles = GetDatFiles(options.InputFiles);
        foreach (var datFile in datFiles)
        {
            var outJSONPath = getJSONPath(datFile, options.Output);
            var rh = ReadDatFile(datFile);
            WriteReplayJson(outJSONPath, rh);
        }
        return 0;
    }

    // Other funcs //////////////////////////////////////////////////////////////////////
    private static List<string> GetDatFiles(IEnumerable<string> paths)
    {
        var files = new List<string>();

        foreach (var path in paths)
        {
            if(File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                var foundFolders = Directory.GetDirectories(path);
                files.AddRange(GetDatFiles(foundFolders));

                var foundFiles = Directory.GetFiles(path);
                foreach (var foundFile in foundFiles)
                {
                    if(Path.HasExtension(".dat")) 
                    {
                        files.Add(foundFile);
                    }
                }
            }
            else
            {
                files.Add(path);
            }
        }

        return files;
    }

    private static string getJSONPath(string inputFile, string outputLoc)
    {
        var inputFileName = Path.GetFileName(inputFile);
        var outputFileName = Path.ChangeExtension(inputFileName, "json");
        var outputPath = Path.Combine(outputLoc, outputFileName);
        return outputPath;
    }

    private static string GetNewDatPath(string outputLoc, string fileName)
    {
        var outputPath = Path.Combine(outputLoc, fileName + ".dat");
        return outputPath;
    }

    private static int CheckOutputPath(string outputLoc, bool createFolders=true)
    {
        if (Path.Exists(outputLoc))
        {
            if (!File.GetAttributes(outputLoc).HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine($"ERR: Output location {outputLoc} already exists as a file. Aborting.");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else if(createFolders)
        {
            try
            {
                Directory.CreateDirectory(outputLoc);
                Console.WriteLine($"Created directory {outputLoc}.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERR: Encountered a(n) {ex.GetType().Name} error while trying to create output directory at {outputLoc} : {ex.Message}");
                return 1;
            }
        }
        Console.WriteLine($"ERR: Provided output directory {outputLoc} does not exist, and createFolders is set to {createFolders}.");
        return 1;
    }

    private static int HandleErrors(IEnumerable<Error> errors)
    {
        return 1;
    }

    static ReplayHeader ReadDatFile(string inputFile)
    {
         return ReplayHeader.FromFile(inputFile);
    }

    static string getJSONPath(string inputFile)
    {
        var fullPath = Path.GetFullPath(inputFile);
        var folder = Path.GetDirectoryName(fullPath);
        var inputFileName = Path.GetFileName(fullPath);
        inputFileName = Path.ChangeExtension(inputFileName, ".json");
        var newFileName = folder + "/json/" + inputFileName;
        return newFileName;
    }

    static void WriteReplayJson(string outputFile, ReplayHeader rh)
    {
        var dirname = Path.GetDirectoryName (outputFile);
        if (!Path.Exists(dirname))
        { 
            System.IO.Directory.CreateDirectory(dirname);
        }

        var replayJson = rh.ToJson();
        File.WriteAllText(outputFile, replayJson);
    }

}
