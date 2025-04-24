using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BBCFReplayConsole.Helpers
{

    /// <summary>
    /// Provides interop with kernel32.dll.
    /// </summary>
    internal static class Kernel32
    {
        public const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        public const uint ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200;

        public const ulong INVALID_HANDLE_VALUE = 0xffffffffffffffff;

        private const string KERNEL32_DLL = "kernel32.dll";

        private const uint STD_OUTPUT_HANDLE = 0xfffffff5;
        private const uint STD_INPUT_HANDLE = 0xfffffff6;

        [DllImport(KERNEL32_DLL, EntryPoint = "GetStdHandle")]
        public static extern IntPtr GetStdHandle(uint handle);

        [DllImport(KERNEL32_DLL, EntryPoint = "GetConsoleMode")]
        public static extern int GetConsoleMode(IntPtr handle, ref uint mode);

        [DllImport(KERNEL32_DLL, EntryPoint = "SetConsoleMode")]
        public static extern int SetConsoleMode(IntPtr handle, uint mode);

        public static IntPtr GetHandleStandardInput() => GetStdHandle(STD_INPUT_HANDLE);

        public static IntPtr GetHandleStandardOutput() => GetStdHandle(STD_OUTPUT_HANDLE);
    }

    /// <summary>
    /// Provides virtual terminal support. Does things like enable ANSI escape sequences.
    /// </summary>
    public static class VirtualTerminal
    {
        /// <summary>
        /// Enables support for ANSI escape sequences.
        /// This method will always return true on platforms other than Windows.
        /// </summary>
        public static bool EnableAnsi()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return true;

            var hStdOut = Kernel32.GetHandleStandardOutput();

            uint outputMode = 0;
            Kernel32.GetConsoleMode(hStdOut, ref outputMode);

            outputMode |= Kernel32.ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            Kernel32.SetConsoleMode(hStdOut, outputMode);

            return true;
        }
    }
    
}
