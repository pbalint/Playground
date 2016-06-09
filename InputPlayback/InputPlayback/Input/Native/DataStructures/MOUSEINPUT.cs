using System;
using System.Runtime.InteropServices;

namespace InputPlayback.Input.Native.DataStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public Int32        X;
        public Int32        Y;
        public UInt32       MouseData;
        public MouseFlag    Flags;
        public UInt32       Time;
        public IntPtr       ExtraInfo;
    }
}