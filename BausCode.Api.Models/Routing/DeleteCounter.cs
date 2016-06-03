using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/counters", "DELETE")]
    public class DeleteCounter : IReturn<DeleteCounterResponse>
    {
    }
}