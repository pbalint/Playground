using InputPlayback.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace InputPlayback
{
    public partial class MainForm : Form
    {
        private List<Actions.Action> actions = new List<Actions.Action>();
        private Worker worker = new Worker();

        public MainForm()
        {
            InitializeComponent();

            worker.RunWorkerCompleted += WorkerStopped;
            worker.ProgressChanged += WorkerProgressChanged;
            worker.Actions = actions;

            columnHeaderParameters.Width = -2;
            foreach (Type actionType in Utils.ActionTypes)
            {
                comboBoxAction.Items.Add(Activator.CreateInstance(actionType));
            }

            if (comboBoxAction.Items.Count > 0)
            {
                comboBoxAction.SelectedIndex = 0;
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Utils.DeserializeActions<List<Actions.Action>>(ref actions, dialog.FileName);
                listViewSteps.Items.Clear();
                for (int i = 0; i < actions.Count; i++)
                {
                    listViewSteps.Items.Add(createListViewItemFromAction(i, actions[i]));
                }
            }
        }

        private ListViewItem createListViewItemFromAction(int index, Actions.Action action)
        {
            ListViewItem item = new ListViewItem(index.ToString());
            item.SubItems.Add(action.ToString());
            StringBuilder parametersBuilder= new StringBuilder();
            foreach (ParameterContainer parameter in action.GetParameters())
            {
                parametersBuilder.Append(parameter.Name).Append(": ").Append(parameter.Value).Append(" ");
            }
            item.SubItems.Add(parametersBuilder.ToString());
            return item;
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Utils.SerializeActions<List<Actions.Action>>(actions, dialog.FileName);
            }
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int currentIndex = (int)e.UserState;
            for (int i = 0; i < listViewSteps.Items.Count; i++)
            {
                if (i == currentIndex)
                {
                    listViewSteps.Items[i].Focused = true;
                    listViewSteps.Items[i].Selected = true;
                }
                else
                {
                    listViewSteps.Items[i].Focused = false;
                    listViewSteps.Items[i].Selected = false;
                }
            }
        }

        private void WorkerStopped(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonPlay.Text = "Play";
        }

        private void comboBoxAction_SelectedValueChanged(object sender, EventArgs e)
        {
            panelActionParameters.buildControlsForAction((Actions.Action)comboBoxAction.SelectedItem);
        }

        private void buttonPlay_Click( object sender, EventArgs e )
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
            else
            {
                worker.RunWorkerAsync();
                buttonPlay.Text = "Stop";
            }
        }

        private void buttonAdd_Click( object sender, EventArgs e )
        {
            Actions.Action action = panelActionParameters.GetActionFromParameters();
            actions.Add( action );
            listViewSteps.Items.Add(createListViewItemFromAction(listViewSteps.Items.Count, action));
        }

        private void buttonUpdate_Click( object sender, EventArgs e )
        {
            Actions.Action action = panelActionParameters.GetActionFromParameters();
            int index = listViewSteps.SelectedIndices[0];
            actions[index] = action;
            listViewSteps.Items[index] = createListViewItemFromAction(index, action);
            listViewSteps.Items[index].Focused = true;
            listViewSteps.Items[index].Selected = true;
        }

        private void buttonDelete_Click( object sender, EventArgs e )
        {
            if (listViewSteps.SelectedIndices.Count <= 0) return;

            int index = listViewSteps.SelectedIndices[0];
            actions.RemoveAt(index);
            listViewSteps.Items.RemoveAt(index);

            if (listViewSteps.Items.Count <= 0) return;

            if (listViewSteps.Items.Count > index)
            {
                listViewSteps.Items[index].Focused = true;
                listViewSteps.Items[index].Selected = true;
            }
            else
            {
                listViewSteps.Items[index - 1].Focused = true;
                listViewSteps.Items[index - 1].Selected = true;
            }
            for (int i = index; i < listViewSteps.Items.Count; i++)
            {
                listViewSteps.Items[i].Text = i.ToString();
            }
        }

        private void mouseTrackerToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (!MouseTracker.Active)
            {
                new MouseTracker().Show();
            }
        }

        private void listViewSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSteps.SelectedIndices.Count <= 0)
            {
                buttonUpdate.Enabled = false;
                return;
            }
            else
            {
                buttonUpdate.Enabled = true;
            }

            panelActionParameters.buildControlsForAction(actions[listViewSteps.SelectedIndices[0]]);
        }
    }
}
