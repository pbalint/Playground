using System.Runtime.InteropServices;

namespace InputPlayback.Input.Native.DataStructures
{
    public struct INPUT
    {
        public InputType type;
        public INPUTUNION u;

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUTUNION
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;

            [FieldOffset(0)]
            public KEYBDINPUT Keyboard;

            [FieldOffset(0)]
            public HARDWAREINPUT Hardware;
        }
    }
}
