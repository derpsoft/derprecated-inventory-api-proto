using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{ProductId}/images/count.json", "GET")]
    public class CountImages : IReturn<CountResponse>
    {
        public long ProductId { get; set; }
    }
}