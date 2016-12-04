namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/orders/{Id}", "GET")]
    public class GetOrder : IReturn<GetOrderResponse>
    {
    }
}
