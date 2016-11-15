using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}/quantity", "GET")]
    public class GetProductQuantityOnHand : IReturn<QuantityOnHandResponse>
    {
        public int Id { get; set; }
    }
}