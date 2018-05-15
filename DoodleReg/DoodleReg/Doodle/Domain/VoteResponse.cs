using Newtonsoft.Json;

namespace DoodleReg.Doodle.Domain
{
    public class VoteResponse
    {
        [JsonProperty("id")]            public int Id { get; set; }
        [JsonProperty("name")]          public string Name { get; set; }
        [JsonProperty("preferences")]   public int[] Preferences { get; set; }
    }
}
