namespace InputPlayback.Actions
{
    public class Sleep : Action
    {
        [Parameter( "Duration", 1 )]
        private int? duration;

        public int? Duration { get { return duration; } set { duration = value; } }

        override
        public void Invoke(Worker.State state)
        {
            if ( duration == null ) return;

            System.Threading.Thread.Sleep( duration?? 0 );
        }
    }
}
