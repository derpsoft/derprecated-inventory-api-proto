namespace BausCode.Api.Jobs.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class User
    {
        [DataMember(Name = "followers_count")]
        public long FollowersCount { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "screen_name")]
        public string ScreenName { get; set; }
    }
}
