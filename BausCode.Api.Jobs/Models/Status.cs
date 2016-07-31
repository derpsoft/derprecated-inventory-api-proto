using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class Status
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "retweet_count")]
        public int RetweetCount { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "entities")]
        public List<Entity> Entities { get; set; }
    }
}