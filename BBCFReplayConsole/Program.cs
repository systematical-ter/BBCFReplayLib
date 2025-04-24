// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Xml;
using BBCFReplayConsole;
using BBCFReplayLib;
using CommandLine;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Exceptions;

using BBCFReplayConsole.Helpers;
using static BBCFReplayConsole.Helpers.Ansi;


class Program
{
    internal static LoggerConfiguration DefaultLoggerConfiguration() => new LoggerConfiguration()
    .MinimumLevel.Is(LogEventLevel.Verbose)
    .Enrich.FromLogContext()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss.fff}][{Level}] {Message:lj}{NewLine}{Exception}", theme: AnsiConsoleTheme.Code
    );

    internal static LoggerConfiguration SecondExampleLoggerConfiguration() => new LoggerConfiguration()
    .Enrich.WithExceptionDetails()
    .MinimumLevel.Is(LogEventLevel.Verbose)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}][{Level}]({SourceContext}) {Message:lj}{NewLine}{Exception}", theme: AnsiConsoleTheme.Literate);

    // Verb definitions //////////////////////////////////////////////////////////////////////
    public class GlobalOptions
    {
        [Option('v', "verbose", Default = false, HelpText = "Output should be more verbose")]
        public bool Verbose { get; set; }

        [Option('i', "input", Required = true, HelpText = "Input files to be processed.")]
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

        [Option('f', "format", Required = true, HelpText = "Formatting string to name files according to. Please see -help for more information.")]
        public string Format { get; set; }
    }

    // Main func //////////////////////////////////////////////////////////////////////
    public static int Main(string[] args)
    {
        VirtualTerminal.EnableAnsi();
        Log.Logger = DefaultLoggerConfiguration().CreateLogger();

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
        List<string> datFilePaths = FileFunctions.GetDatFiles(options.InputFiles);

        Console.WriteLine($"Found {datFilePaths.Count} data files -- continue? [{GREEN}y{RESET}/{RED_BOLD}N{RESET}]");
        var toContinue = Console.ReadLine();
        if(toContinue != "y") 
        {
            Log.Information("Aborting...");
            return 0; 
        }
        Log.Information("Continuing...");

        foreach (string datFilePath in datFilePaths)
        {
            var rh = FileFunctions.ReadDatFile(datFilePath);
            var varMaps = ReplayRenamingTools.MapVariableValues(variableNames, rh);
            var newName = ReplayRenamingTools.CreateNewName(options.Format, varMaps);
            var response = FileFunctions.ValidateOutputPath(options.Output);
            if (response != 0) 
            {
                Log.Error("Encountered an error while trying to rename files.");
                return response; 
            }
            var path = FileFunctions.GetNewDatPath(options.Output, newName);
            if(FileFunctions.DatFileExists(path))
            {
                var checksumInHand = FileFunctions.CalculateChecksum(datFilePath);
                var checksumOnDisk = FileFunctions.CalculateChecksum(path);

                if(checksumInHand != checksumOnDisk)
                {
                    Log.Information($"Name collision between two disparate replay files, named {newName}. Renaming new file.");
                    newName = FileFunctions.FixNameCollision(newName);
                    path = FileFunctions.GetNewDatPath(options.Output, newName);
                }
                else
                {
                    Log.Information($"Name collision between identical files. Skipping new copy of {newName}.");
                    continue;
                }
            }
            CopyDatFile(path, datFilePath, rh);
        }

        Log.Information($"Done. {GREEN}{datFilePaths.Count}{RESET} files written to {CYAN}{options.Output}{RESET}. Exiting.");
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
        List<string> datFiles = FileFunctions.GetDatFiles(options.InputFiles);
        foreach (var datFile in datFiles)
        {
            var outJSONPath = FileFunctions.GetJSONPath(datFile, options.Output);
            var rh = FileFunctions.ReadDatFile(datFile);
            FileFunctions.WriteReplayJson(outJSONPath, rh);
        }
        return 0;
    }

    // Other funcs //////////////////////////////////////////////////////////////////////


    private static int HandleErrors(IEnumerable<Error> errors)
    {
        return 1;
    }



}
