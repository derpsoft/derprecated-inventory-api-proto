using System.Runtime.Serialization;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "screen_name")]
        public string ScreenName { get; set; }

        [DataMember(Name = "followers_count")]
        public long FollowersCount { get; set; }
    }
}