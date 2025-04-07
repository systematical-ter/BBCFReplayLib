using System.Globalization;
using System.Text;
using System.Text.Json;

namespace BBCFReplayLib
{
    public class ReplayHeader
    {
        private const int HEADER_OFFSET = 0x08;
        // if we're being passed just a header byte array, we'll need to
        //      subtract the header offset from all of these addresses.
        private const int HEADER_SIZE = 0x390;

        private const int CHECKSUM = 0x00;              // first 2 bytes are checksum w/ a 00 00 buffer after
        private const int UNKNOWN_1 = 0x04;             // some uint
        private const int UNKNOWN_2 = 0x08;             // some uint
                                                        // technically 04+08 can be a ulong?
        private const int UNKNOWN_3 = 0x0c;             // some uint
        private const int VALID_FLAG_OFFSET = 0x10;     // 4 bytes
        // 4 empty bytes
        private const int DATE1_OFFSET = 0x18;          // look at ReplayDate class below for breakdown of Date offsets.
        // 4 empty bytes.
        private const int DATE2_OFFSET = 0x58;          // look at ReplayDate class below for breakdown of Date offsets.
        // 4 empty bytes.
        private const int WINNER_OFFSET = 0x98;         // was labelled "winner_maybe" by IM
        private const int P1_OFFSET = 0x9C;             // look at ReplayPlayerID for offsets
        // 00 00 buffer until the start of P2_OFFSET
        //  todo: keep an eye on this
        private const int P2_OFFSET = 0x166;            // look at ReplayPlayerID for offsets
        // 00 00 buffer until the start of P1_CHAR_OFFSET
        //  todo: keep an eye on this
        private const int P1_CHAR_OFFSET = 0x230;       // uint
        private const int P2_CHAR_OFFSET = 0x234;       // uint
        private const int RECORDER_OFFSET = 0x238;      // look at ReplayPlayerID for offsets
        // 00 00 buffer until UNKNOWN_... ;
        private const int UNKNOWN_x304 = 0x304;         // some uint: have seen: [1,1,1]
        private const int UNKNOWN_x308 = 0x308;         // some uint: have seen: [6,6,6]
        // 8 empty bytes
        private const int P1_LEVEL_OFFSET = 0x314;      // uint
        private const int P2_LEVEL_OFFSET = 0x318;      // uint
        private const int UNKNOWN_x31c = 0x31c;         // some uint: have seen: [1,1,1]
        // empty bytes
        private const int ROUNDS_TO_WIN_OFFSET_MAYBE = 0x398;       //uint
        private const int SECONDS_PER_ROUND_OFFSET_MAYBE = 0x39c;   //uint
        private const int STAGE_MUSIC_1_OFFSET = 0x3a0; // uint, stage or music?
        private const int STAGE_MUSIC_2_OFFSET = 0x3a4; // ^
        private const int P1_UNKNOWN_OFFSET = 0x3a8;    // look at ReplayPlayerUnknown for offsets
        private const int P2_UNKNOWN_OFFSET = 0x414;    // look at ReplayPlayerUnknown for offsets

        private uint _valid;
        private ReplayDate _date1;
        private ReplayDate _date2;
        private uint _winnerFlag;
        private ReplayPlayerID _p1;
        private ReplayPlayerID _p2;
        private uint _p1Char;
        private uint _p2Char;
        private ReplayPlayerID _recorder;
        private uint _unknown_x304;
        private uint _unknown_x308;
        private uint _p1Level; // was labelled "minus one"?
        private uint _p2Level;
        private uint _unknown_x31c;
        private uint _roundsToWin;
        private uint _secondsPerRound;
        private uint _stageOrMusic1;
        private uint _stageOrMusic2;
        private ReplayPlayerUnknown _p1Unknown;
        private ReplayPlayerUnknown _p2Unknown;


        public DateTime Date1 { get; private set; } = DateTime.Now;
        public DateTime Date2 { get; private set; } = DateTime.Now;

        public bool IsValid => _valid != 0;

        // ... and I'm not dealing with the rest yet.

        public byte[] _headerBinary = new byte[HEADER_SIZE];

        public string JsonString => JsonSerializer.Serialize(this);

        public static ReplayHeader FromFile(string filePath)
        {
            byte[] bytes;
            using (var fs = File.OpenRead(filePath))
            using (var br = new BinaryReader(fs))
            {
                br.BaseStream.Seek(HEADER_OFFSET, SeekOrigin.Begin);
                bytes = br.ReadBytes(HEADER_SIZE);
            }

            return FromHeaderBytes(bytes);

        }

        public static ReplayHeader FromHeaderBytes(byte[] byteArray)
        {
            var header = new ReplayHeader();
            using (var ms = new MemoryStream(byteArray))
            using (var br = new BinaryReader(ms))
            {
                // yeet the entire header into storage first.
                header._headerBinary = br.ReadBytes(HEADER_SIZE);
                br.BaseStream.Seek(0, SeekOrigin.Begin);

                _ = br.ReadBytes(0x08);
                header._valid = br.ReadUInt32();
                _ = br.ReadBytes(4);

                var date1Bytes = br.ReadBytes(ReplayDate.ByteSize);
                header._date1 = ReplayDate.FromBytes(date1Bytes);

                _ = br.ReadBytes(4);

                var date2Bytes = br.ReadBytes(ReplayDate.ByteSize);
                header._date2 = ReplayDate.FromBytes(date2Bytes);

                _ = br.ReadBytes(4);

                header._winnerFlag = br.ReadUInt32();

                var p1Bytes = br.ReadBytes(ReplayPlayerID.ByteSize);
                header._p1 = ReplayPlayerID.FromBytes(p1Bytes);

                // I ran into weird things trying to guarantee the size of the buffer between these
                //  and after p2 when I tried to then apply the same structure to the recorder data.
                //  so I'm just not trying to read the region at all and will just keep an eye
                //  to make sure there's no data there, ig
                br.BaseStream.Seek(P2_OFFSET, SeekOrigin.Current);
                var p2Bytes = br.ReadBytes(ReplayPlayerID.ByteSize);
                header._p2 = ReplayPlayerID.FromBytes(p2Bytes);

                // again, just forcefully seek
                br.BaseStream.Seek(P1_CHAR_OFFSET, SeekOrigin.Current);
                header._p1Char = br.ReadUInt32();
                header._p2Char = br.ReadUInt32();

                var recorderBytes = br.ReadBytes(ReplayPlayerID.ByteSize);
                header._recorder = ReplayPlayerID.FromBytes(recorderBytes);

                br.BaseStream.Seek(UNKNOWN_x304, SeekOrigin.Current);
                header._unknown_x304 = br.ReadUInt32();
                header._unknown_x308 = br.ReadUInt32();

                br.BaseStream.Seek(P1_LEVEL_OFFSET, SeekOrigin.Current);
                header._p1Level = br.ReadUInt32();
                header._p2Level = br.ReadUInt32();
                header._unknown_x31c = br.ReadUInt32();

                br.BaseStream.Seek(ROUNDS_TO_WIN_OFFSET_MAYBE, SeekOrigin.Current);
                header._roundsToWin = br.ReadUInt32();
                header._secondsPerRound = br.ReadUInt32();
                header._stageOrMusic1 = br.ReadUInt32();
                header._stageOrMusic2 = br.ReadUInt32();

                var p1UnknownBytes = br.ReadBytes(ReplayPlayerUnknown.ByteSize);
                header._p1Unknown = ReplayPlayerUnknown.FromBytes(p1UnknownBytes);

                br.BaseStream.Seek(P2_UNKNOWN_OFFSET, SeekOrigin.Current);
                var p2UnknownBytes = br.ReadBytes(ReplayPlayerUnknown.ByteSize);
                header._p2Unknown = ReplayPlayerUnknown.FromBytes(p2UnknownBytes);

            }
            return header;
        }

    }

    class ReplayDate
    {
        // informative offset storage. They're all packed one after another,
        //      so you don't actually need this to read them.
        //      (except PADDING_SIZE, of course. It's just 4 bytes tho.)
        private const int UNIX_OFFSET       = 0;
        private const int PADDING           = 0x4;
        private const int YEAR_OFFSET       = 0x8;
        private const int MONTH_OFFSET      = 0xc;
        private const int DAY_OFFSET        = 0x10;
        private const int HOUR_OFFSET       = 0x14;
        private const int MINUTE_OFFSET     = 0x18;
        private const int SECOND_OFFSET     = 0x1c;
        private const int CHAR_REP_OFFSET   = 0x20;
        // "hey systematical isn't this just 3 ways of representing the same data?"
        //      sure is. and the same date is stored twice, so we have this information
        //      6 times!
        // Thanks, BlazBlue.
        // (I'm storing all 6 just to be careful.)

        private const int PADDING_SIZE      = 0x4;
        private const int CHAR_REP_SIZE     = 0x18;

        private ulong _unixTimestamp;
        private uint _year;
        private uint _month;
        private uint _day;
        private uint _hour;
        private uint _minute;
        private uint _second;
        private char[] _charRep = new char[CHAR_REP_SIZE];

        public DateTime Date { get; private set; }
        public static implicit operator DateTime(ReplayDate rd) => rd.Date;

        // total # of bytes needed by this object to get all its info
        public const int ByteSize = CHAR_REP_OFFSET + CHAR_REP_SIZE;

        public static ReplayDate FromBytes(byte[] bytes)
        {
            var rd = new ReplayDate();
            using (var ms = new MemoryStream(bytes))
            using (var br = new BinaryReader(ms))
            {
                rd._unixTimestamp = br.ReadUInt64();
                _ = br.ReadBytes(PADDING_SIZE);
                rd._year = br.ReadUInt32();
                rd._month = br.ReadUInt32();
                rd._day = br.ReadUInt32();
                rd._hour = br.ReadUInt32();
                rd._minute = br.ReadUInt32();
                rd._second = br.ReadUInt32();
                rd._charRep = br.ReadChars(CHAR_REP_SIZE);

                rd.Date = new DateTime((long)rd._unixTimestamp);

            }
            return rd;
        }
    }

    class ReplayPlayerID()
    {
        private const int STEAMID_OFFSET = 0x0;
        private const int NAME_OFFSET = 0x8;
        private const int NAME_SIZE = 0x12 * 2;
        // names are max length 0x12, but with 2 bytes per character.

        private ulong _steamID;
        private byte[] _unicodeName = new byte[0x24];

        public string Name { get; private set; }
        public static implicit operator string(ReplayPlayerID rpi) => rpi.Name;

        public const int ByteSize = NAME_OFFSET + NAME_SIZE;

        public static ReplayPlayerID FromBytes(byte[] bytes)
        {
            var rpi = new ReplayPlayerID();
            using (var ms = new MemoryStream(bytes))
            using (var br = new BinaryReader(ms))
            {
                rpi._steamID = br.ReadUInt64();
                rpi._unicodeName = br.ReadBytes(0x24);

                // they write names down very lazily. if the name is shorter than what was
                //  saved in that file before, they don't bother zeroing out the leftovers.
                //  therefore you have to cut on the FIRST terminator you see.
                rpi.Name = Encoding.Unicode.GetString(rpi._unicodeName).Split('\0')[0];
            }

            return rpi;
        }
    }

    class ReplayPlayerUnknown()
    {
        // This definitely is SOMETHING, but what it is, I don't know.
        //  So far on the 3 replays I've checked, _character has matched p1_char and p2_char
        // I haven't decoded what unknown 2 & 3 are yet--
        // Unknown 2 has been: (can be inconsistent across entries in a file)
        //             [(1,5)(1,5), -> replay3
        //              (1,5)(1,5), -> replay7
        //              (0,5)(0,11) -> replay11
        //              ]
        // Unknown1 has been: (consistent across both entries in a file)
        //              [25604,     -> replay3
        //               25604,     -> replay7
        //               25600      -> replay11
        // maybe has something to do with intro animation? replay11 was ter vs ragna
        private const int UNKNOWN_1_OFFSET = 0x0;
        private const int CHARACTER_OFFSET = 0x8;
        private const int UNKNOWN_2_OFFSET = 0x14;
        private const int UNKNOWN_3_OFFSET = 0x18;

        private uint _unknown1;
        private uint _character;
        private uint _unknown2;
        private uint _unknown3;

        public const int ByteSize = UNKNOWN_3_OFFSET + 4; // 4 = size of uint

        public static ReplayPlayerUnknown FromBytes(byte[] bytes)
        {
            var rpu = new ReplayPlayerUnknown();
            using (var ms = new MemoryStream(bytes))
            using (var br = new BinaryReader(ms))
            {
                rpu._unknown1 = br.ReadUInt32();
                _ = br.ReadUInt32();
                rpu._character = br.ReadUInt32();
                _ = br.ReadUInt32();
                _ = br.ReadUInt32();
                rpu._unknown2 = br.ReadUInt32();
                rpu._unknown3 = br.ReadUInt32();
            }
            return rpu;
        }
    }

    class ReplayRound()
    {
        /* Example data block: (3th int on is consistent between replays 6,7,11 for the first round.)
         *
         * 00000480 92  79  f3  03    uint       66288018
         * 00000484 66  dc  cc  90    uint       2429344870
         * 00000488 d9  11  00  00    uint       4569
         * 0000048c 01  00  00  00    uint       1
         * 00000490 01  00  00  00    uint       1 # p1 has burst
         * 00000494 00  00  00  00    uint       0
         * 00000498 01  00  00  00    uint       1 # p2 has burst
         * 0000049c 00  00  00  00    uint       0
         * 000004a0 a0  86  01  00    uint       100000
         *                      burst meter?
         * 000004a4 a0  86  01  00    uint       100000
         *                      burst meter p2?
         */
        // can be either 2 uints or a ulong... not sure which yet
        // might be some unique round id, b/c it exists for empty rounds and is not
        //  a checksum because it DIFFERS between empty rounds.
        // may just have to fuck with the values to figure out what they're for,
        //  because I think there needs to be an "animation skip" counter somewhere,
        //  but I don't know where it'd be.
        private const int UNKNOWN_1_OFFSET = 0x0;
        private const int UNKNOWN_2_OFFSET = 0x4;


        // HYPOTHESIS:
        //  unknown3 is # of (frames of?) inputs to process
        //  unknown4 is the winner of the round
        // REASONING:
        //  replay6, unknown 4 is 1     -> 0    -> 1
        //           unknown 3 is 4569  -> 3933 -> 3207
        //  replay7, unknown 4 is 1     -> 1    -> 1
        //           unknown 3 is 4507  -> 4909 -> 0 (+burst meters at 30k, which is odd)
        //  replay11,unknown 4 is 1     -> 1    -> 1
        //           unknown 3 is 3719  -> 2490 -> 0
        //  ... and in all 3 of these replays, p1 is the winner
        
        // used to be unknown 3
        private const int N_ENTRIES_OFFSET = 0x8;

        // used to be unknown 4
        private const int WINNER_OFFSET = 0xc;

        // I'm _fairly_ certain about these, but I still need to check them.
        private const int P1_BURST_OFFSET = 0x10;
        private const int P2_BURST_OFFSET = 0x18;
        private const int P1_BURST_COUNTER_OFFSET = 0x20;
        private const int P2_BURST_COUNTER_OFFSET = 0x24;

    }
}
