namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/status", "GET")]
    public class GetQueueStatus : IReturn<GetQueueStatusResponse>
    {
    }
}
