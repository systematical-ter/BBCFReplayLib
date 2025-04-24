namespace BBCFReplayConsole.Helpers
{
    internal static class Ansi
    {
        public enum ColorType
        {
            Unknown = 0,
            Foreground = 1,
            Background = 2,
        }

        /// <summary>
        /// Default ANSI escape sequence.
        /// </summary>
        public const string ESC = "\x1b[";

        /// <summary>
        /// Resets formatting.
        /// </summary>
        public const string RESET = ESC + "0m";

        public const string UNDERLINE = ESC + "4" + SEQ_END_GRAPHICAL;

        public const string BLACK = ESC + FG_COLOR_BLACK + SEQ_END_GRAPHICAL;
        public const string BLACK_BOLD = ESC + BOLD_INLINE + FG_COLOR_BLACK + SEQ_END_GRAPHICAL;

        public const string RED = ESC + FG_COLOR_RED + SEQ_END_GRAPHICAL;
        public const string RED_BOLD = ESC + BOLD_INLINE + FG_COLOR_RED + SEQ_END_GRAPHICAL;

        public const string GREEN = ESC + FG_COLOR_GREEN + SEQ_END_GRAPHICAL;
        public const string GREEN_BOLD = ESC + BOLD_INLINE + FG_COLOR_GREEN + SEQ_END_GRAPHICAL;

        public const string YELLOW = ESC + FG_COLOR_YELLOW + SEQ_END_GRAPHICAL;
        public const string YELLOW_BOLD = ESC + BOLD_INLINE + FG_COLOR_YELLOW + SEQ_END_GRAPHICAL;

        public const string CYAN = ESC + FG_COLOR_CYAN + SEQ_END_GRAPHICAL;
        public const string CYAN_BOLD = ESC + BOLD_INLINE + FG_COLOR_CYAN + SEQ_END_GRAPHICAL;

        public const string WHITE = ESC + FG_COLOR_WHITE + SEQ_END_GRAPHICAL;
        public const string WHITE_BOLD = ESC + BOLD_INLINE + FG_COLOR_WHITE + SEQ_END_GRAPHICAL;

        private const string BOLD_INLINE = "1;";
        private const string UNDERLINE_INLINE = "4;";

        private const string FG_COLOR_BLACK = "30";
        private const string FG_COLOR_RED = "31";
        private const string FG_COLOR_GREEN = "32";
        private const string FG_COLOR_YELLOW = "33";
        private const string FG_COLOR_BLUE = "34";
        private const string FG_COLOR_MAGENTA = "35";
        private const string FG_COLOR_CYAN = "36";
        private const string FG_COLOR_WHITE = "37";

        private const string BG_8_BIT = "48:5:";
        private const string FG_8_BIT = "38:5:";

        private const string SEQ_END_GRAPHICAL = "m";

        /// <summary>
        /// Returns the ANSI sequence for an 8-bit color value. Refer to the linked Wikipedia article
        /// for what values map to which colors.
        /// <para/>
        /// <see href="https://en.wikipedia.org/wiki/ANSI_escape_code#8-bit">ANSI Escape Codes: 8-bit</see>
        /// </summary>
        public static string Color8Bit(byte value, ColorType type = ColorType.Foreground) =>
            type switch
            {
                ColorType.Foreground => $"{ESC}{FG_8_BIT}{value}{SEQ_END_GRAPHICAL}",
                ColorType.Background => $"{ESC}{BG_8_BIT}{value}{SEQ_END_GRAPHICAL}",
                _ => "", // Color type isn't valid, so we'll skip everything.
            };
    }
}