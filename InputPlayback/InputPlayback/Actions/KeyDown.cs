using InputPlayback.Input.Native;
using InputPlayback.Input.Native.DataStructures;
using System;
using System.Runtime.InteropServices;

namespace InputPlayback.Actions
{
    public class KeyDown : Action
    {
        [Parameter("Key", 1)]
        private VirtualKeyCode? keyCode;

        public VirtualKeyCode? KeyCode { get { return keyCode; } set { keyCode = value; } }

        override
        public void Invoke(Worker.State state)
        {
            INPUT[] queue = new INPUT[1];

            queue[0] = new INPUT();
            queue[0].type = InputType.Keyboard;
            queue[0].u.Keyboard.KeyCode = keyCode?? VirtualKeyCode.SPACE;
            queue[0].u.Keyboard.ExtraInfo = IntPtr.Zero;

            uint ret = NativeMethods.SendInput(1, queue, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
