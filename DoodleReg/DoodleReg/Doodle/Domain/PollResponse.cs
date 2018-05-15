using Newtonsoft.Json;

namespace DoodleReg.Doodle.Domain
{
    public class PollResponse
    {
        public class PollInitiator
        {
            [JsonProperty("name")]              public string Name { get; set; }
            [JsonProperty("email")]             public string Email { get; set; }
            [JsonProperty("notify")]            public bool Notify { get; set; }
            [JsonProperty("avatarLargeUrl")]    public string AvatarLargeUrl { get; set; }
            [JsonProperty("AvatarSmallUrl")]    public string AvatarSmallUrl { get; set; }
            [JsonProperty("userId")]            public string UserId { get; set; }
        }
        public class PollOptions
        {
            [JsonProperty("start")]             public long Start { get; set; }
            [JsonProperty("date")]              public long Date { get; set; }
            [JsonProperty("allDay")]            public bool AllDay { get; set; }
            [JsonProperty("available")]         public bool Available { get; set; }
        }
        public class PollParticipant
        {
            [JsonProperty("id")]                public long Id { get; set; }
            [JsonProperty("name")]              public string Name { get; set; }
            [JsonProperty("options")]           public string[] Options { get; set; }
        }

        [JsonProperty("id")]                    public string Id { get; set; }
        [JsonProperty("adminKey")]              public string AdminKey { get; set; }
        [JsonProperty("latestChange")]          public long LatestChange { get; set; }
        [JsonProperty("initiated")]             public long Initiated { get; set; }
        [JsonProperty("participantsCount")]     public long ParticipantsCount { get; set; }
        [JsonProperty("inviteesCount")]         public long InviteesCount { get; set; }
        [JsonProperty("type")]                  public string Type { get; set; }
        [JsonProperty("columnConstraint")]      public long ColumnConstraint { get; set; }
        [JsonProperty("rowConstraint")]         public long RowConstraint { get; set; }
        [JsonProperty("preferencesType")]       public string PreferencesType { get; set; }
        [JsonProperty("state")]                 public string State { get; set; }
        [JsonProperty("locale")]                public string Locale { get; set; }
        [JsonProperty("title")]                 public string Title { get; set; }
        [JsonProperty("initiator")]             public PollInitiator Initiator { get; set; }
        [JsonProperty("options")]               public PollOptions[] Options { get; set; }
        [JsonProperty("participants")]          public PollParticipant[] Participants { get; set; }
        [JsonProperty("optionsHash")]           public string OptionsHash { get; set; }
        [JsonProperty("device")]                public string Device { get; set; }
        [JsonProperty("levels")]                public string Levels { get; set; }
    }
}
