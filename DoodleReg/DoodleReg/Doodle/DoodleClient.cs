using DoodleReg.Doodle.Domain;
using log4net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DoodleReg.Doodle
{
    public class DoodleClient
    {
        private static readonly ILog LOG = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string DOODLE_URL = "https://doodle.com/api/v2.0/polls/";
        private static readonly string VOTE_ENDPOINT = "/participants";

        private HttpClient http_client = new HttpClient();

        public async Task<PollResponse> GetPollResponseAsync( string poll_id )
        {
            var response = await http_client.GetAsync( DOODLE_URL + poll_id );
            string response_string = await response.Content.ReadAsStringAsync();
            LOG.InfoFormat( "Received poll: {0}: {1}", poll_id, response_string );
            PollResponse poll = JsonConvert.DeserializeObject<PollResponse>( response_string );
            return poll;
        }

        public PollResponse GetPollResponse( string poll_id )
        {
            var response = GetPollResponseAsync( poll_id );
            response.Wait();
            return response.Result;
        }

        public async Task<VoteResponse> VoteAsync( string poll_id, VoteRequest request )
        {
            string request_string = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent( request_string, Encoding.UTF8, "application/json" );
            LOG.InfoFormat( "Voting: {0}", request_string );
            var response = await http_client.PostAsync( DOODLE_URL + poll_id + VOTE_ENDPOINT, content );
            string response_string = await response.Content.ReadAsStringAsync();
            LOG.InfoFormat( "Success: {0}", response_string );
            VoteResponse vote_response = JsonConvert.DeserializeObject< VoteResponse >( response_string );
            return vote_response;
        }

        public VoteResponse Vote( string poll_id, VoteRequest request )
        {
            var response_task = VoteAsync( poll_id, request );
            response_task.Wait();
            return response_task.Result;
        }
    }
}
