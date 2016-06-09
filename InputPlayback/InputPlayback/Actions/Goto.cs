namespace InputPlayback.Actions
{
    public class Goto : Action
    {
        [Parameter("Line", 1)]
        private int? line;

        public int? Line { get { return line; } set { line = value; } }

        override
        public void Invoke(Worker.State state)
        {
            if (line == null) return;

            state.currentAction = line - 1?? 0;
        }
    }
}
