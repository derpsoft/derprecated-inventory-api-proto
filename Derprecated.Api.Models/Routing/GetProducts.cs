namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products", "GET")]
    [Authenticate]
    public class GetProducts : IReturn<GetProductsResponse>
    {
        public bool? MetaOnly { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
