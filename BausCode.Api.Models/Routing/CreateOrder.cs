using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/orders", "POST")]
    public class CreateOrder : IReturn<CreateOrderResponse>
    {
    }
}