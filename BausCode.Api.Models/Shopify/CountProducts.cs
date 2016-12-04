namespace BausCode.Api.Models.Shopify
{
    using ServiceStack;

    [Route("/admin/products/count.json", "GET")]
    public class CountProducts : IReturn<CountResponse>
    {
    }
}
