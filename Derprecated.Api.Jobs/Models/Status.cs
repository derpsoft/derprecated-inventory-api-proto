namespace Derprecated.Api.Jobs.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Status
    {
        [DataMember(Name = "entities")]
        public List<Entity> Entities { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "retweet_count")]
        public int RetweetCount { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}
