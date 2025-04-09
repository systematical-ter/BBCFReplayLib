namespace BBCFReplayLib.Tests
{
    internal class TestHelper
    {
        private const string EXAMPLE_REPLAY_DIR_NAME = "ExampleReplays";

        public static DirectoryInfo ExampleReplaysDirectory { get; }

        private static DirectoryInfo RootDirectory { get; }

        static TestHelper()
        {
            var asmPath = Path.GetDirectoryName(typeof(TestHelper).Assembly.Location) ?? ".\\";
            RootDirectory = new DirectoryInfo(asmPath);
            ExampleReplaysDirectory = new DirectoryInfo(Path.Combine(
                RootDirectory.FullName, EXAMPLE_REPLAY_DIR_NAME));
        }

        public static string GetReplayFilePath(string fileName, bool ensureExists = true)
        {
            var path = Path.Combine(ExampleReplaysDirectory.FullName, fileName);
            if (ensureExists && !File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"The replay file at '{path}' does not exist!");
            }

            return path;
        }
    }
}
