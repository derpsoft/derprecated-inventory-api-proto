using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{ProductId}.json", "GET")]
    public class GetProduct : IReturn<ProductResponse>
    {
        public long ProductId { get; set; }
    }
}