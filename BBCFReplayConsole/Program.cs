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
        List<string> datFiles = GetDatFiles(options.InputFiles);
        foreach (string datFile in datFiles)
        {
            var rh = ReadInputFile(datFile);
            
        }
        return 0;
    }

    private static int RunJson(JsonOptions options)
    {
        List<string> datFiles = GetDatFiles(options.InputFiles);
        foreach (var datFile in datFiles)
        {
            var outJSONPath = getJSONPath(datFile, options.Output);
            var rh = ReadInputFile(datFile);
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

    private static int HandleErrors(IEnumerable<Error> errors)
    {
        return 1;
    }

    static ReplayHeader ReadInputFile(string inputFile)
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
