using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/count.json", "GET")]
    public class CountProducts : IReturn<CountResponse>
    {
    }
}