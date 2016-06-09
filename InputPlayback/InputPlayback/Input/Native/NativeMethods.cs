using InputPlayback.Input.Native.DataStructures;
using System;
using System.Runtime.InteropServices;

namespace InputPlayback.Input.Native
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int16 GetAsyncKeyState(UInt16 virtualKeyCode);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int16 GetKeyState(UInt16 virtualKeyCode);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(UInt32 numberOfInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] inputs, Int32 sizeOfInputStructure);

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);
    }
}