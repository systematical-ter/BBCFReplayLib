using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BBCFReplayLib.Tests
{
    public class ReplayHeaderUnitTests(ITestOutputHelper output)
    {
        private ITestOutputHelper Output { get; } = output;

        /// <summary>
        /// Simple test to just make sure nothing breaks while reading the file.
        /// </summary>
        [Theory]
        [InlineData("vsAveryChu.dat")]
        [InlineData("vsDGF1.dat")]
        [InlineData("vsTubazo.dat")]
        [InlineData("vsZander.dat")]
        public void FromFileReadsFileSuccessfully(string fileName)
        {
            var replayPath = TestHelper.GetReplayFilePath(fileName);

            Output.WriteLine("Reading replay {0}...", fileName);
            var actual = ReplayHeader.FromFile(replayPath);
            Assert.True(actual.IsValid);
        }
    }
}
