using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BBCFReplayLib
{
    internal static class BinaryReaderExtensions
    {
        public static void EnsureEmpty(this BinaryReader reader, int count)
        {
            var data = reader.ReadBytes(count);
            if (data.All(x => x == 0)) return;

            var curPos = reader.BaseStream.Position;
            var startPos = curPos - count;
            throw new InvalidDataException($"Provided region {startPos:x4} - {curPos:x4} contains data!");
        }
        public static void EnsureEmptyUntil(this BinaryReader reader, int endPosition, int headerOffset = 0)
        {
            var startPos = reader.BaseStream.Position;
            var amount = (long)endPosition - headerOffset - startPos;

            var data = reader.ReadBytes((int)amount);
            if (data.All(x => x == 0)) return;

            var curPos = reader.BaseStream.Position;
            throw new InvalidDataException($"Provided region {startPos:x4} - {curPos:x4} contains data!");

        }
    }

}
