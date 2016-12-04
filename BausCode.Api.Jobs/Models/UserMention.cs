namespace BausCode.Api.Jobs.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UserMention
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "screen_name")]
        public string ScreenName { get; set; }
    }
}
