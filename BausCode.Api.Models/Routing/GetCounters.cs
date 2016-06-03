using System.Collections.Generic;
using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/counters", "GET")]
    public class GetCounters : IReturn<List<GetCountersResponse>>
    {
    }
}
