using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/status", "GET")]
    public class GetQueueStatus : IReturn<GetQueueStatusResponse>
    {
    }
}