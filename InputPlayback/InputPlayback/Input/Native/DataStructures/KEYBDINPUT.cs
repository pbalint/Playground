using System;
using System.Runtime.InteropServices;

namespace InputPlayback.Input.Native.DataStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public VirtualKeyCode   KeyCode;
        public ushort           Scan;
        public KeyboardFlag     Flags;
        public uint             Time;
        public IntPtr           ExtraInfo;
    }
}
