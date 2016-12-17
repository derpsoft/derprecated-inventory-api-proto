namespace Derprecated.Api.Jobs.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SearchMetadata
    {
        [DataMember(Name = "count")]
        public long Count { get; set; }

        [DataMember(Name = "max_id")]
        public long MaxId { get; set; }

        [DataMember(Name = "since_id")]
        public long SinceId { get; set; }
    }
}
