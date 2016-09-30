using ServiceStack;

namespace BausCode.Api.Models.Routing.Shopify
{
    [Route("/admin/products/count.json", "GET")]
    public class GetProductsCount : IReturn<GetProductsCountResponse>
    {
    }
}