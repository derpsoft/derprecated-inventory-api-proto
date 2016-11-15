using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{Id}.json", "GET")]
    public class GetProduct : IReturn<ProductResponse>
    {
        public long Id { get; set; }
    }
}