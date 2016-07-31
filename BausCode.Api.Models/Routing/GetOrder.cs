using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/orders/{Id}", "GET")]
    public class GetOrder : IReturn<GetOrderResponse>
    {
    }
}