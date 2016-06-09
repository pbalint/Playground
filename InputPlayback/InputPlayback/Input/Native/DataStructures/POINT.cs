using System;
using System.Runtime.InteropServices;

namespace InputPlayback.Input.Native.DataStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public Int32 x;
        public Int32 y;
    }
}
