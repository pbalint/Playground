using InputPlayback.Input.Native;
using InputPlayback.Input.Native.DataStructures;
using System;
using System.Runtime.InteropServices;

namespace InputPlayback.Actions
{
    public class KeyClick : Action
    {
        [Parameter("Key", 1)]
        private VirtualKeyCode? keyCode;

        [Parameter("ScanCode", 2)]
        private bool? scanCode;

        public VirtualKeyCode? KeyCode { get { return keyCode; } set { keyCode = value; } }

        public bool? ScanCode { get { return scanCode; } set { scanCode = value; } }

        override
        public void Invoke(Worker.State state)
        {
            INPUT[] queue = new INPUT[2];

            queue[0] = new INPUT();
            queue[0].type = InputType.Keyboard;
            queue[0].u.Keyboard.KeyCode = keyCode?? VirtualKeyCode.SPACE;
            queue[0].u.Keyboard.ExtraInfo = IntPtr.Zero;

            queue[1] = new INPUT();
            queue[1].type = InputType.Keyboard;
            queue[1].u.Keyboard.KeyCode = keyCode ?? VirtualKeyCode.SPACE;
            queue[1].u.Keyboard.Flags = KeyboardFlag.KeyUp;
            queue[1].u.Keyboard.ExtraInfo = IntPtr.Zero;

            if (scanCode?? false)
            {
                Utils.VkToScan( queue[ 0 ].u.Keyboard );
                Utils.VkToScan( queue[ 1 ].u.Keyboard );
            }
            NativeMethods.SendInput( 2, queue, Marshal.SizeOf( typeof( INPUT ) ) );
        }
    }
}
