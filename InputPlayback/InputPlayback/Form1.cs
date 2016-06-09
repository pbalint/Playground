using System;
using System.Drawing;
using System.Windows.Forms;
using InputPlayback.Actions;
using System.Collections.Generic;

namespace InputPlayback
{
    public partial class MainForm : Form
    {
        Timer timer = new Timer();

        public MainForm()
        {
            InitializeComponent();

            timer.Interval = 200;
            timer.Tick += timer_Tick;
            //timer.Start();
            columnHeaderIndex.Width = -1;
            columnHeaderDescription.Width = -1;

            comboBoxAction.Items.Add( new KeyDown() );
            comboBoxAction.Items.Add( new KeyUp() );
            comboBoxAction.Items.Add( new MouseDown() );
            comboBoxAction.Items.Add( new MouseUp() );
            comboBoxAction.Items.Add( new MouseClick() );
            comboBoxAction.Items.Add( new MouseMove() );
            comboBoxAction.Items.Add( new Sleep() );
            comboBoxAction.Items.Add( new TypeText() );
        }

        void timer_Tick( object sender, EventArgs e )
        {
            /*Native.INPUT[] queue = new Native.INPUT[2];
            queue[0] = new Native.INPUT();
            queue[1] = new Native.INPUT();

            queue[0].Type = (uint)Native.InputType.Mouse;
            queue[1].Type = (uint)Native.InputType.Mouse;

            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            queue[0].Data.Mouse.X = 65535 * ( x + 1 ) / Screen.PrimaryScreen.Bounds.Width;
            queue[0].Data.Mouse.Y = 65535 * ( y + 1 ) / Screen.PrimaryScreen.Bounds.Height;
            queue[1].Data.Mouse.X = Cursor.Position.X;
            queue[1].Data.Mouse.Y = Cursor.Position.Y;
            queue[0].Data.Mouse.Flags = (UInt32)(Native.MouseFlag.Move | Native.MouseFlag.Absolute);
            queue[1].Data.Mouse.Flags = (UInt32)(Native.MouseFlag.Move | Native.MouseFlag.Absolute);

            /*queue[0].Data.Mouse.Flags |= (uint)Native.MouseFlag.LeftDown;
            queue[1].Data.Mouse.Flags |= (uint)Native.MouseFlag.LeftUp;*/
            //Native.NativeMethods.SendInput(1, queue, Marshal.SizeOf(typeof(Native.INPUT)));
        }

        private void menuItemExit_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void buttonPlay_Click( object sender, EventArgs e )
        {

        }

        private void buttonAdd_Click( object sender, EventArgs e )
        {

        }

        private void buttonEdit_Click( object sender, EventArgs e )
        {

        }

        private void buttonDelete_Click( object sender, EventArgs e )
        {

        }

        private void listViewsteps_SelectedIndexChanged( object sender, EventArgs e )
        {

        }

        private void comboBoxAction_SelectedValueChanged( object sender, EventArgs e )
        {
            panelActionParameters.Controls.Clear();
            foreach (Control control in buildControlsForAction( (InputPlayback.Actions.Action)comboBoxAction.SelectedItem ))
            {
                panelActionParameters.Controls.Add( control );
            }
        }

        private List<Control> buildControlsForAction( InputPlayback.Actions.Action action )
        {
            List<Control> controls = new List<Control>();
            foreach (Tuple<string, object> parameter in action.GetParameters() )
            {
                Label label = new Label();
                label.Text = parameter.Item1;
                label.Anchor = AnchorStyles.Left;
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.AutoSize = true;
                controls.Add( label );

                Control control = getControlForType( parameter.Item2.GetType() );
                controls.Add( control );
            }
            return controls;
        }

        private Control getControlForType( Type type )
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return new NumericUpDown();
                default:
                    return new TextBox();
            }
        }

        private void mouseTrackerToolStripMenuItem_Click( object sender, EventArgs e )
        {

        }
    }
}
