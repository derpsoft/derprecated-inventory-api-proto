using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class SearchTweetsResponse
    {
        [DataMember(Name = "statuses")]
        public List<Status> Statuses { get; set; }

        [DataMember(Name = "search_metadata")]
        public SearchMetadata SearchMetadata { get; set; }
    }
}