namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/{Id}/quantity", "GET")]
    [Authenticate]
    public class GetProductQuantityOnHand : IReturn<QuantityOnHandResponse>
    {
        public int Id { get; set; }
    }
}
