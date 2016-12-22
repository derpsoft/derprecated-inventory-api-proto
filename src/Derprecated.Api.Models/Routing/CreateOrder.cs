namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/orders", "POST")]
    [Authenticate]

    public class CreateOrder : IReturn<OrderResponse>
    {
    }
}
