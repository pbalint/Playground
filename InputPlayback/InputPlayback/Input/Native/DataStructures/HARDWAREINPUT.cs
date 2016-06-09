using System;
using System.Runtime.InteropServices;

namespace InputPlayback.Input.Native.DataStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public UInt32 Msg;
        public UInt16 ParamL;
        public UInt16 ParamH;
    }
}
