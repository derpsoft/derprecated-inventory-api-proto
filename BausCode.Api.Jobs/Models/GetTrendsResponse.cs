using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class GetTrendsResponse
    {
        [DataMember(Name = "as_of")]
        public DateTime AsOf { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "locations")]
        public List<Location> Locations { get; set; }

        [DataMember(Name = "trends")]
        public List<Trend> Trends { get; set; }
    }
}