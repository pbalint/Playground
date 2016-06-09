using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InputPlayback
{
    public partial class ParameterPanel : UserControl
    {
        private List<Tuple<string, Control>> controlsForParameters = new List<Tuple<string, Control>>();
        private Type actionType;

        public ParameterPanel()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private Control GetControlForType( Type type, object value )
        {
            Control control;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = type.GetGenericArguments()[0];
            }
            if (type.IsPrimitive)
            {
                control = BuildTextBox(value);
            }
            else if (type.IsEnum)
            {
                control = BuildEnumSelector(type, value);
            }
            else
            {
                control = BuildTextBox(value);
            }
            return control;
        }

        private Control BuildTextBox(object value)
        {
            Control control = new TextBox();
            //control.Dock = DockStyle.Fill;
            if (value != null)
            {
                control.Text = value.ToString();
            }
            return control;
        }

        private Control BuildEnumSelector(Type type, object value)
        {
            ComboBox control = null;
            Type enumType = null;
            if (type.IsEnum)
            {
                enumType = type;
            }
            if (enumType != null)
            {
                control = new ComboBox();
                control.DropDownStyle = ComboBoxStyle.DropDownList;
                List<String> sortedEnumNames = new List<string>(Enum.GetNames(enumType));
                sortedEnumNames.Sort();
                int indexToSelect = 0;
                for (int i = 0; i < sortedEnumNames.Count; i++)
                {
                    if (value != null && sortedEnumNames[i] == value.ToString())
                    {
                        indexToSelect = i;
                    }
                    control.Items.Add(sortedEnumNames[i]);
                }
                if (control.Items.Count > 0)
                {
                    control.SelectedIndex = indexToSelect;
                }
            }
            return control;
        }

        public void buildControlsForAction( InputPlayback.Actions.Action action )
        {
            panelParameters.SuspendLayout();

            controlsForParameters.Clear();
            panelParameters.Controls.Clear();

            actionType = action.GetType();
            foreach ( Actions.ParameterContainer parameter in action.GetParameters() )
            {
                Label label = new Label();
                label.Text = parameter.Name;
                label.Anchor = AnchorStyles.Left;
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.AutoSize = true;
                panelParameters.Controls.Add( label );

                Control control = GetControlForType( parameter.Type, parameter.Value );
                panelParameters.Controls.Add( control );

                controlsForParameters.Add( new Tuple<string, Control>( parameter.Name, control ) );
            }

            panelParameters.ResumeLayout();
        }

        public Actions.Action GetActionFromParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (Tuple<string, Control> entry in controlsForParameters)
            {
                parameters.Add( entry.Item1, entry.Item2.Text );
            }
            Actions.Action action = (Actions.Action)Activator.CreateInstance( actionType );
            action.SetParameters( parameters );
            return action;
        }
    }
}
