using System;

namespace InputPlayback.Input.Native.DataStructures
{
    [Flags]
    public enum KeyboardFlag : uint
    {
        None        = 0,
        ExtendedKey = 0x0001,
        KeyUp       = 0x0002,
        Unicode     = 0x0004,
        ScanCode    = 0x0008
    }
}
