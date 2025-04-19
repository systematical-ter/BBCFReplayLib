using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BBCFReplayLib;

namespace BBCFReplayConsole
{
    internal class ReplayRenamingTools
    {
        private static Regex RegexVarPattern = new(
            @"(?<variable>{[a-zA-Z0-9]+})",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static MatchCollection GetVariables(string variableNameString)
        {
            return RegexVarPattern.Matches(variableNameString);
        }

        public static Dictionary<string, string> MapVariableValues(MatchCollection variableNames, ReplayHeader replayHeader)
        {
            var variableValues = new Dictionary<string, string>();

            foreach(Match m in variableNames)
            {
                var (vName, vValue) = GetVariableValue(m.Value, replayHeader);
                variableValues[vName] = vValue;
            }

            return variableValues;
        }

        private static (string variableName, string variableValue) GetVariableValue(string variableName, ReplayHeader replayHeader)
        {
            switch(variableName)
            {
                case "p1name":
                    return(variableName, replayHeader.P1.Name);
                case "p2name":
                    return(variableName, replayHeader.P2.Name);
                case "date":
                    return(variableName, replayHeader.Date1.ToShortTimeString());
                case "p1char":
                    return(variableName, replayHeader.GetP1CharName());
                case "p2char":
                    return(variableName, replayHeader.GetP2CharName());
                case "winner":
                    return (variableName, replayHeader.Winner.ToString());
                default:
                    throw new ArgumentException($"Unknown variable name {variableName} provided.");
            }
            
        }

    }
}
