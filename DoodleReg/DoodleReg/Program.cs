using DoodleReg.Doodle;
using DoodleReg.Doodle.Domain;
using log4net;
using Microsoft.Office.Interop.Outlook;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DoodleReg
{
    class Program
    {
        private static readonly ILog LOG = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Regex DOODLE_POLL_REGEXP = new Regex(@"doodle.com/poll/([a-z0-9]+)");
        private static DoodleClient doodle_client = new DoodleClient();

        public string GetPollId( string text )
        {
            Match match = DOODLE_POLL_REGEXP.Match(text);
            string poll_id = null;
            if ( match.Success )
            {
                poll_id = match.Groups[ 1 ].Value;
            }
            return poll_id;
        }

        public void VoteForPoll( string poll_id )
        {
            PollResponse poll = doodle_client.GetPollResponse(poll_id);

            int vote_index = poll.Options.Length - 2;
            while ( vote_index >= 0 )
            {
                if ( poll.Options[ vote_index ].Available ) break;
                vote_index--;
            }
            if ( vote_index < 0 )
            {
                if ( poll.Options[ poll.Options.Length - 1 ].Available )
                {
                    vote_index = poll.Options.Length - 1;
                }
            }
            if ( vote_index >= 0 )
            {
                int[] votes = new int[ poll.Options.Length ];
                votes[ vote_index ] = 1;
                VoteRequest vote_request = new VoteRequest( "Pámer Bálint", votes, poll.OptionsHash );
                VoteResponse vote_response = doodle_client.Vote( poll_id, vote_request );
            }
        }

        private void VoteForPollInEmail()
        {
            Application application = new Application();

            NameSpace name_space = application.GetNamespace("mapi");
            name_space.Logon( Missing.Value, Missing.Value, false, true );
            MAPIFolder inbox = name_space.GetDefaultFolder(OlDefaultFolders.olFolderInbox);

            Items items = inbox.Items;
            items.Sort( "[ReceivedTime]", true );

            MailItem msg = (MailItem)items.GetFirst();
            LOG.InfoFormat( "Searching for unread mails..." );
            while ( msg != null && msg.UnRead )
            {
                LOG.InfoFormat( "Checking email: {0}", msg.Subject );
                string poll_id = GetPollId( msg.Body );
                if ( poll_id != null )
                {
                    LOG.InfoFormat( "Found poll with id: {0}", poll_id );
                    VoteForPoll( poll_id );
                }

                msg.UnRead = false;
                msg = items.GetNext();
            }
            name_space.Logoff();
        }

        static void Main( string[] args )
        {
            try
            {
                new Program().VoteForPollInEmail();
            }
            catch ( System.Exception e )
            {
                LOG.ErrorFormat( "{0} Exception caught: ", e );
            }
        }
    }
}
