// See https://aka.ms/new-console-template for more information

using BBCFReplayLib;
using CommandLine;

class Program
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('i', "inputfile", Required = true, HelpText = "Input REPLAY file.")]
        public string InputFile { get; set; }
    }

    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
        .WithParsed<Options>(o =>
        {
            var rh = ReadInputFile(o.InputFile);
            var outputFile = GetNewPath(o.InputFile);
            WriteReplayJson(outputFile, rh);
        });
    }

    static ReplayHeader ReadInputFile(string inputFile)
    {
         return ReplayHeader.FromFile(inputFile);
    }

    static string GetNewPath(string inputFile)
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
