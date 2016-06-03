using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/counters", "POST")]
    public class PostCounter : IReturn<PostCounterResponse>
    {
        public string Term { get; set; }
    }
}