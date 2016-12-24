namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products", "GET")]
    [Authenticate]
    public class GetProducts : IReturn<GetProductsResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }
}
