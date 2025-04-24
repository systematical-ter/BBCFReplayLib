using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BBCFReplayLib;
using Serilog;

namespace BBCFReplayConsole.Helpers
{
    public class FileFunctions
    {
        private static Regex RegexRenameCountPattern = new(
            @"\((?<renameCount>{[0-9]+})\)\.dat^",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        internal static List<string> GetDatFiles(IEnumerable<string> paths)
        {
            var files = new List<string>();

            foreach (var path in paths)
            {
                // TODO : fails if doesn't exist.
                if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    if (Path.HasExtension(".dat"))
                    {
                        files.Add(path);
                    }
                    continue;
                }

                var foundFolders = Directory.GetDirectories(path);
                files.AddRange(GetDatFiles(foundFolders));

                var foundFiles = Directory.GetFiles(path);
                foreach (var foundFile in foundFiles)
                {
                    if (Path.HasExtension(".dat"))
                    {
                        files.Add(foundFile);
                    }
                }
            }

            return files;
        }
        internal static string GetJSONPath(string inputFile, string outputLoc)
        {
            var inputFileName = Path.GetFileName(inputFile);
            var outputFileName = Path.ChangeExtension(inputFileName, "json");
            var outputPath = Path.Combine(outputLoc, outputFileName);
            return outputPath;
        }


        internal static string GetJSONPath(string inputFile)
        {
            var fullPath = Path.GetFullPath(inputFile);
            var folder = Path.GetDirectoryName(fullPath);
            var inputFileName = Path.GetFileName(fullPath);
            inputFileName = Path.ChangeExtension(inputFileName, ".json");
            var newFileName = folder + "/json/" + inputFileName;
            return newFileName;
        }

        internal static string GetNewDatPath(string outputLoc, string fileName)
        {
            var outputPath = Path.Combine(outputLoc, fileName + ".dat");
            return outputPath;
        }

        internal static int ValidateOutputPath(string outputLoc, bool createFolders = true)
        {
            if (Path.Exists(outputLoc))
            {
                if (!File.GetAttributes(outputLoc).HasFlag(FileAttributes.Directory))
                {
                    Log.Error($"ERR: Output location {outputLoc} already exists as a file. Aborting.");
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else if (createFolders)
            {
                try
                {
                    Directory.CreateDirectory(outputLoc);
                    Log.Information($"Created directory {outputLoc}.");
                    return 0;
                }
                catch (Exception ex)
                {
                    Log.Error($"Encountered a(n) {ex.GetType().Name} error while trying to create output directory at {outputLoc} : {ex.Message}");
                    return 1;
                }
            }
            Log.Error($"Provided output directory {outputLoc} does not exist, and createFolders is set to {createFolders}.");
            return 1;
        }

        internal static ReplayHeader ReadDatFile(string inputFile)
        {
            return ReplayHeader.FromFile(inputFile);
        }

        internal static void WriteReplayJson(string outputFile, ReplayHeader rh)
        {
            var dirname = Path.GetDirectoryName(outputFile);
            if (!Path.Exists(dirname))
            {
                Directory.CreateDirectory(dirname);
            }

            var replayJson = rh.ToJson();
            File.WriteAllText(outputFile, replayJson);
        }

        // Collision validation funcs
        internal static string CalculateChecksum(string path)
        {
            using var sha256 = SHA256.Create();
            using var fs = File.OpenRead(path);
            var hashBytes = sha256.ComputeHash(fs);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        internal static bool DatFileExists(string path)
        {
            return File.Exists(path);
        }

        public static string FixNameCollision(string newName)
        {
            var renameCountMatch = RegexRenameCountPattern.Match(newName);
            if (renameCountMatch.Success)
            {
                if (int.TryParse(renameCountMatch.Value, out var renameCount))
                {
                    renameCount = int.Parse(renameCountMatch.Value);
                }
                else
                {
                    Log.Debug("DEBUG: Somehow, in FixNameCollision, the rename count found a match but parsing to an int failed. Defaulting to 0.");
                }

                renameCount += 1;
                var newerName = RegexRenameCountPattern.Replace(newName, renameCount.ToString());
                return newerName;
            }

            return Path.GetFileNameWithoutExtension(newName) + "(1).dat";
        }
    }
}
