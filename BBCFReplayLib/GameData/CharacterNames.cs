using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCFReplayLib.GameData
{
    internal class CharacterNames
    {
        public static string GetName(int charID)
        {
            switch (charID)
            {
                case 0:
                    return "ragna";
                case 1:
                    return "jin";
                case 2:
                    return "noel";
                case 3:
                    return "rachel";
                case 4:
                    return "taokaka";
                case 5:
                    return "tager";
                case 6:
                    return "litchi";
                case 7:
                    return "arakune";
                case 8:
                    return "bang";
                case 9:
                    return "carl";
                case 10:
                    return "hakumen";
                case 11:
                    return "nu";
                case 12:
                    return "tsubaki";
                case 13:
                    return "hazama";
                case 14:
                    return "mu";
                case 15:
                    return "makoto";
                case 16:
                    return "valkenhayn";
                case 17:
                    return "platinum";
                case 18:
                    return "relius";
                case 19:
                    return "izayoi";
                case 20:
                    return "amane";
                case 21:
                    return "bullet";
                case 22:
                    return "azrael";
                case 23:
                    return "kagura";
                case 24:
                    return "kokonoe";
                case 25:
                    return "terumi";
                case 26:
                    return "celica";
                case 27:
                    return "lambda";
                case 28:
                    return "hibiki";
                case 29:
                    return "nine";
                case 30:
                    return "naoto";
                case 31:
                    return "izanami";
                case 32:
                    return "susanoo";
                case 33:
                    return "es";
                case 34:
                    return "mai";
                case 35:
                    return "jubei";
                default:
                    throw new NotImplementedException($"Provided charID {charID} has not yet been assigned to a character.");
                }
        }

        public static string GetShortName(int charID)
        {
            switch (charID)
            {
                // todo:
                // make sure these actually align with internal
                case 0:
                    return "rg";
                case 1:
                    return "jn";
                case 2:
                    return "no";
                case 3:
                    return "rc";
                case 4:
                    return "to";
                case 5:
                    return "ta";
                case 6:
                    return "li";
                case 7:
                    return "ar";
                case 8:
                    return "ba";
                case 9:
                    return "ca";
                case 10:
                    return "ha";
                case 11:
                    return "nu";
                case 12:
                    return "ts";
                case 13:
                    return "hz";
                case 14:
                    return "mu";
                case 15:
                    return "ma";
                case 16:
                    return "va";
                case 17:
                    return "pl";
                case 18:
                    return "re";
                case 19:
                    return "iz";
                case 20:
                    return "am";
                case 21:
                    return "bu";
                case 22:
                    return "az";
                case 23:
                    return "ka";
                case 24:
                    return "ko";
                case 25:
                    return "tm";
                case 26:
                    return "ce";
                case 27:
                    return "la";
                case 28:
                    return "hi";
                case 29:
                    return "ni";
                case 30:
                    return "na";
                case 31:
                    return "in";
                // todo:
                // missing susanoo and catman
                default:
                    throw new NotImplementedException($"Provided charID {charID} has not yet been assigned to a character shortname.");
            }
        }
    }
}
