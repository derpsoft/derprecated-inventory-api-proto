using System.Runtime.Serialization;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class UserMention
    {
        [DataMember(Name = "screen_name")]
        public string ScreenName { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}