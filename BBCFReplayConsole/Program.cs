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
        var folder = Path.GetDirectoryName(inputFile);
        var inputFileName = Path.GetFileName(inputFile);
        var newFileName = folder + "/json/" + inputFileName;
        return newFileName;
    }

    static void WriteReplayJson(string outputFile, ReplayHeader rh)
    {
        var replayJson = rh.JsonString;
        File.WriteAllText(outputFile, replayJson);
    }

}
