using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCFReplayLib
{
    internal class Consts
    {
        public static string GetCharName(int charID)
        {
            switch (charID)
            {
                case 0:
                    return "ragna";
                case 7:
                    return "arakune";
                case 25:
                    return "terumi";
                default:
                    throw new NotImplementedException($"Provided charID {charID} has not yet been assigned to a character.");
            }
        }

        public static string GetCharShortName(int charID)
        {
            switch (charID)
            {
                case 0:
                    return "rg";
                case 7:
                    return "ar";
                case 25:
                    return "tm";
                default:
                    throw new NotImplementedException($"Provided charID {charID} has not yet been assigned to a character shortname.");
            }
        }
    }
}
