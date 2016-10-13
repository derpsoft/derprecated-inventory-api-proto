using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/search", "POST")]
    public class SearchProducts : IReturn<SearchProductsResponse>
    {
        public string Query { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}