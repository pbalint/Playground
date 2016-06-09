using System.Collections.Generic;
using System.ComponentModel;

namespace InputPlayback
{
    public class Worker : BackgroundWorker
    {
        public class State
        {
            public int currentAction = 0;
        }

        private State state = new State();
        private List<Actions.Action> actions = new List<Actions.Action>();
        public List<Actions.Action> Actions { set {  actions = value; } }

        public Worker()
        {
            DoWork += PlayBackInput;
            WorkerSupportsCancellation = true;
            WorkerReportsProgress = true;
        }

        private void PlayBackInput(object sender, DoWorkEventArgs e)
        {
            while (!CancellationPending && state.currentAction < actions.Count)
            {
                ReportProgress(0, state.currentAction);
                actions[state.currentAction].Invoke(state);
                state.currentAction++;
            }
            state.currentAction = 0;
        }
    }
}
