using Xunit.Abstractions;
using BBCFReplayConsole.Helpers;

namespace BBCFReplayConsole.Tests
{
    public class ReplayHeaderUnitTests(ITestOutputHelper output)
    {
        private ITestOutputHelper Output { get; } = output;

        /// <summary>
        /// Test to make sure the rename collision functionality functions as expected.
        /// </summary>
        [Theory]
        [InlineData("Replay.dat",               "Replay(1).dat")]
        [InlineData("Replay(1).dat",            "Replay(2).dat")]
        [InlineData("Replay(2).dat",            "Replay(3).dat")]
        [InlineData("Replay(12).dat",           "Replay(13).dat")]
        [InlineData("Replay(2)(1).dat",         "Replay(2)(2).dat")]
        [InlineData("char(1)_char(2)_date.dat", "char(1)_char(2)_date(1).dat")]
        public void TestNameCollisionResolution(string fileName, string expectedResult)
        {
            Output.WriteLine($"Testing {fileName} to {expectedResult}");
            var res = FileFunctions.FixNameCollision(fileName);
            Assert.Equal(expectedResult, res);
        }
    }
}