namespace Derprecated.Api.Jobs.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Entity
    {
        [DataMember(Name = "user_mentions")]
        public List<UserMention> UserMentions { get; set; }
    }
}
