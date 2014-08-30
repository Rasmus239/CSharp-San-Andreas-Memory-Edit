using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SA_GiveMoney
{
    class Program
    {
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(
            string lpClassName,
            string lpWindowName
        );

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            UIntPtr nSize,
            out IntPtr lpNumberOfBytesWritten
        );

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            Int32 bInheritHandle,
            UInt32 dwProcessId
        );

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(
            IntPtr hWnd,
            out uint lpdwProcessId
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static unsafe extern bool ReadProcessMemory(
         IntPtr hProcess,
         IntPtr lpBaseAddress,
         void* lpBuffer,
         int dwSize,
         out IntPtr lpNumberOfBytesRead
        );

        static void Main(string[] args)
        {
            uint amount = 99999999;
            EditMemory(0xB7CE50, amount);
        }

        public static bool EditMemory(int Address, uint Value)
        {
            UInt32 ProcID;
            IntPtr bytesout;
            IntPtr WindowHandle = FindWindow(null, "GTA: San Andreas");
            if (WindowHandle == null) { return false; }
            GetWindowThreadProcessId(WindowHandle, out ProcID);
            IntPtr ProcessHandle = OpenProcess(0x1F0FFF, 1, ProcID);
            WriteProcessMemory(ProcessHandle, (IntPtr)Address, BitConverter.GetBytes(Value), (UIntPtr)sizeof(uint), out bytesout);
            return true;
        }
    }
}
