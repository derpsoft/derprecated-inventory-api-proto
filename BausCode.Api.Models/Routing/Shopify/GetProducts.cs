using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Routing.Shopify
{
    [Route("/admin/products.json", "GET")]
    public class GetProducts : IReturn<GetProductsResponse>
    {
        [Alias("limit")]
        public int Limit { get; set; }
    }
}