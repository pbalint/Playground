using InputPlayback.Input.Native.DataStructures;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InputPlayback
{
    public partial class MouseTracker : Form
    {
        private Timer timer = new Timer();
        private static bool active = false;
        public static bool Active { get { return active; } }

        public MouseTracker()
        {
            InitializeComponent();

            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();
            active = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CURSORINFO info;
            info.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
            bool b = InputPlayback.Input.Native.NativeMethods.GetCursorInfo(out info);
            lMousePosition.Text = "X:" + info.ptScreenPos.x + " Y: " + info.ptScreenPos.y;
        }

        private void MouseTracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            active = false;
        }
    }
}
