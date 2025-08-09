using BBCFReplayLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCFReplayApp.Tools
{
    internal class FileMgmt
    {
        static public string[] CollectFromChaosFolders(string rootFolderPath)
        {

            // Folder is expected to contain folders named using numbers, in increasing order.
            // Each of these folders may then either be:
            //  - A backup of the entire "Save" folder
            //  - The Replay folder and replay_list.dat

            // The goal is to collect all of the replays,
            //  sort only to the unique replays,
            //  save only the unique replays into a new folder.

            string[] foundFolders = Directory.GetDirectories(rootFolderPath);
            string[] foundFiles = [];

            foreach(string folder in foundFolders)
            {
                string[] innerFoundFolders = Directory.GetDirectories(folder);
                if (innerFoundFolders != null && innerFoundFolders.Contains(Path.Join(folder, "Save")))
                {
                    foundFiles = foundFiles.Concat(Directory.GetFiles(Path.Join(folder, "Save", "Replay"), "replay*.dat")).ToArray();
                }
                else if (innerFoundFolders != null && innerFoundFolders.Contains(Path.Join(folder, "Replay")))
                {
                    foundFiles = foundFiles.Concat(Directory.GetFiles(Path.Join(folder, "Replay"), "replay*.dat")).ToArray();
                }
                else if (innerFoundFolders != null)
                {
                    foundFiles = foundFiles.Concat(Directory.GetFiles(folder, "replay*.dat")).ToArray();
                }
                else
                {
                    Console.Error.WriteLine("Encountered empty folder: " + folder);
                }
            }

            return foundFiles;
        }

        static public List<(ReplayHeader,string)> FilterToUnique(string[] foundReplayFiles)
        {
            HashSet<ReplayHeader> ret = new HashSet<ReplayHeader>();
            List<(ReplayHeader,string)> uniqueReplayFiles = new List<(ReplayHeader,string)>();

            foreach (string replayFile in foundReplayFiles)
            {
                ReplayHeader t = ReplayHeader.FromFile(replayFile);
                if (ret.Add(ReplayHeader.FromFile(replayFile)))
                {
                    uniqueReplayFiles.Add((t,replayFile));
                }
            }

            return uniqueReplayFiles;
        }

        static public void CopyFiles(List<(ReplayHeader,string)> ReplayFiles, string CopyToPath)
        {
            foreach ((ReplayHeader replayHeader, string replayFile) in ReplayFiles)
            {
                string newName = replayHeader.Date1.ToString("MM_dd_yyyy_HH_mm") + "_" + replayHeader.P1.SteamID + "_" + replayHeader.P2.SteamID +".dat";
                File.Copy(replayFile, Path.Join(CopyToPath, newName));
            }
        }
        // TODO: Implement a "REPAIR" method
        // TODO: Implement a "DELETE DUPLICATES" method
    }
}
