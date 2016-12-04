namespace BausCode.Api.Jobs.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class SearchTweetsResponse
    {
        [DataMember(Name = "search_metadata")]
        public SearchMetadata SearchMetadata { get; set; }

        [DataMember(Name = "statuses")]
        public List<Status> Statuses { get; set; }
    }
}
