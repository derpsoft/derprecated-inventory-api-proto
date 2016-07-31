using System.Runtime.Serialization;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class SearchMetadata
    {
        [DataMember(Name = "max_id")]
        public long MaxId { get; set; }

        [DataMember(Name = "since_id")]
        public long SinceId { get; set; }

        [DataMember(Name = "count")]
        public long Count { get; set; }
    }
}