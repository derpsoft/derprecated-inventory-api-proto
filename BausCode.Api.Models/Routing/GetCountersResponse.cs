using System;

namespace BausCode.Api.Models.Routing
{
    public class GetCountersResponse
    {
        public string Term { get; set; }
        public long Counter { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}