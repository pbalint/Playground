using Newtonsoft.Json;

namespace DoodleReg.Doodle.Domain
{
    public class VoteRequest
    {
        [JsonProperty("name")]               public string Name { get; set; }
        [JsonProperty("optionsHash")]        public string OptionsHash { get; set; }
        [JsonProperty("participantKey")]     public string ParticipantKey { get; set; }
        [JsonProperty("preferences")]        public int[] Preferences { get; set; }

        public VoteRequest(string name, int[] votes, string options_hash)
        {
            Name = name;
            Preferences = votes;
            OptionsHash = options_hash;
        }
    }
}
