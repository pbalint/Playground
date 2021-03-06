﻿using InputPlayback.Input.Native;
using InputPlayback.Input.Native.DataStructures;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InputPlayback.Actions
{
    public class MouseClick : Action
    {
        [Parameter("Button", 1)]
        private MouseButtons? button;

        [Parameter("DoubleClick", 2)]
        private bool? doubleClick;

        public MouseButtons? Button { get { return button; } set { button = value; } }

        public bool? DoubleClick { get { return doubleClick; } set { doubleClick = value; } }

        override
        public void Invoke(Worker.State state)
        {
            INPUT[] queue = new INPUT[2];
            queue[0] = new INPUT();
            queue[1] = new INPUT();

            queue[0].type = InputType.Mouse;
            queue[1].type = InputType.Mouse;

            switch (button)
            {
                case MouseButtons.Left:
                    queue[0].u.Mouse.Flags |= MouseFlag.LeftDown;
                    queue[1].u.Mouse.Flags |= MouseFlag.LeftUp;
                    break;
                case MouseButtons.Right:
                    queue[0].u.Mouse.Flags |= MouseFlag.RightDown;
                    queue[1].u.Mouse.Flags |= MouseFlag.RightUp;
                    break;
                case MouseButtons.Middle:
                    queue[0].u.Mouse.Flags |= MouseFlag.MiddleDown;
                    queue[1].u.Mouse.Flags |= MouseFlag.MiddleUp;
                    break;
                case MouseButtons.XButton1:
                    queue[0].u.Mouse.Flags |= MouseFlag.XDown;
                    queue[1].u.Mouse.Flags |= MouseFlag.XUp;
                    queue[0].u.Mouse.MouseData |= (ushort)XButton.XButton1;
                    queue[1].u.Mouse.MouseData |= (ushort)XButton.XButton1;
                    break;
                case MouseButtons.XButton2:
                    queue[0].u.Mouse.Flags |= MouseFlag.XDown;
                    queue[1].u.Mouse.Flags |= MouseFlag.XUp;
                    queue[0].u.Mouse.MouseData |= (ushort)XButton.XButton2;
                    queue[1].u.Mouse.MouseData |= (ushort)XButton.XButton2;
                    break;
            }
            NativeMethods.SendInput(2, queue, Marshal.SizeOf(typeof(INPUT)));
            if ( doubleClick?? false )
            {
                NativeMethods.SendInput( 2, queue, Marshal.SizeOf( typeof( INPUT ) ) );
            }
        }
    }
}
