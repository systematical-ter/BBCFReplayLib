using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BBCFReplayLib.Tests
{
    public class ExampleReplayQuickparse(ITestOutputHelper output)
    {
        private ITestOutputHelper Output { get; } = output;

        private static string GetReplayJSONFilePath(string fileName)
        {
            var workingDirectory = Environment.CurrentDirectory;

            var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var examplesDirectory = Path.Combine(projectDirectory, "ExampleReplays");
            var jsonPath = Path.Combine(examplesDirectory, "JSON");
            var jsonName = Path.ChangeExtension(fileName, "json");
            var path = Path.Combine(jsonPath, jsonName);
            return path;
        }

        private static void WriteReplayHeaderJSON(ReplayHeader rh, string fileName)
        {
            var replayJson = rh.ToJson();
            File.WriteAllText(fileName, replayJson);
        }

        [Theory]
        [InlineData("vsAveryChu.dat")]
        [InlineData("vsDGF1.dat")]
        [InlineData("vsTubazo.dat")]
        [InlineData("vsZander.dat")]
        public void WriteJSON(string fileName)
        {
            var replayPath = TestHelper.GetReplayFilePath(fileName);

            Output.WriteLine("Reading replay {0}...", fileName);
            var rh = ReplayHeader.FromFile(replayPath);

            var jsonPath = GetReplayJSONFilePath(fileName);

            Output.WriteLine("Writing replay to {0}...", jsonPath);
            WriteReplayHeaderJSON(rh, jsonPath);

        }
    }
}
