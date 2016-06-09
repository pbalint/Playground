using InputPlayback.Input.Native;
using InputPlayback.Input.Native.DataStructures;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InputPlayback.Actions
{
    public class MouseMove : Action
    {
        [Parameter( "X", 1) ]
        private int? x;
        [Parameter( "Y", 2 )]
        private int? y;

        public int? X { get { return x; } set { x = value; } }
        public int? Y { get { return y; } set { y = value; } }

        override
        public void Invoke(Worker.State state)
        {
            INPUT[] queue = new INPUT[1];
            queue[0] = new INPUT();

            queue[0].type = InputType.Mouse;

            queue[0].u.Mouse.X = 65535 * ((x??0) + 1) / Screen.PrimaryScreen.Bounds.Width;
            queue[0].u.Mouse.Y = 65535 * ((y??0) + 1) / Screen.PrimaryScreen.Bounds.Height;
            queue[0].u.Mouse.Flags = MouseFlag.Move | MouseFlag.Absolute;

            NativeMethods.SendInput(1, queue, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
