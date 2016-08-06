using InputPlayback.Actions;
using System.Text;
using System.Windows.Forms;

namespace InputPlayback
{
    class ActionItem : ListViewItem
    {
        private Action action;
        public Action Action { get { return action; } }

        public ActionItem(int index, Actions.Action action )
        {
            this.action = action;

            Text = index.ToString();
            SubItems.Add( action.ToString() );
            StringBuilder parametersBuilder= new StringBuilder();
            foreach ( ParameterContainer parameter in action.GetParameters() )
            {
                parametersBuilder.Append( parameter.Name ).Append( ": " ).Append( parameter.Value ).Append( " " );
            }
            SubItems.Add( parametersBuilder.ToString() );
        }
    }
}
