namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/orders/{Id}", "GET")]
    [Authenticate]
    public class GetOrder : IReturn<GetOrderResponse>
    {
    }
}
