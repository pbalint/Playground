using InputPlayback.Input.Native;
using InputPlayback.Input.Native.DataStructures;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InputPlayback.Actions
{
    public class MouseUp : Action
    {
        [Parameter("Button", 1)]
        private MouseButtons? button;

        public MouseButtons? Button { get { return button; } set { button = value; } }

        override
        public void Invoke(Worker.State state)
        {
            INPUT[] queue = new INPUT[1];
            queue[0] = new INPUT();

            queue[0].type = InputType.Mouse;

            switch (button)
            {
                case MouseButtons.Left:
                    queue[0].u.Mouse.Flags |= MouseFlag.LeftUp;
                    break;
                case MouseButtons.Right:
                    queue[0].u.Mouse.Flags |= MouseFlag.RightUp;
                    break;
                case MouseButtons.Middle:
                    queue[0].u.Mouse.Flags |= MouseFlag.MiddleUp;
                    break;
                case MouseButtons.XButton1:
                    queue[0].u.Mouse.Flags |= MouseFlag.XUp;
                    queue[0].u.Mouse.MouseData |= (ushort)XButton.XButton1;
                    break;
                case MouseButtons.XButton2:
                    queue[0].u.Mouse.Flags |= MouseFlag.XUp;
                    queue[0].u.Mouse.MouseData |= (ushort)XButton.XButton2;
                    break;
            }
            NativeMethods.SendInput(1, queue, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
