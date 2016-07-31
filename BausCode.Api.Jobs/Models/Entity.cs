using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class Entity
    {
        [DataMember(Name = "user_mentions")]
        public List<UserMention> UserMentions { get; set; }
    }
}