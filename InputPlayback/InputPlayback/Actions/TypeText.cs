using InputPlayback.Input.Native;
using InputPlayback.Input.Native.DataStructures;
using System;
using System.Runtime.InteropServices;

namespace InputPlayback.Actions
{
    public class TypeText : Action
    {
        [Parameter("Text", 1)]
        private string text;

        public string Text { get { return text; } set { text = value; } }

        override
        public void Invoke(Worker.State state)
        {
            foreach (char ch in text)
            {
                TypeChar(ch);
            }
        }

        private void TypeChar(char ch)
        {
            INPUT[] queue = new INPUT[2];

            queue[0] = new INPUT();
            queue[0].type = InputType.Keyboard;
            queue[0].u.Keyboard = Utils.GetKeyInputFromChar(ch);

            queue[1] = new INPUT();
            queue[1].type = InputType.Keyboard;
            queue[1].u.Keyboard = Utils.GetKeyInputFromChar(ch);
            queue[1].u.Keyboard.Flags |= KeyboardFlag.KeyUp;

            uint ret = NativeMethods.SendInput(2, queue, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
