namespace Derprecated.Api.Models.Shopify
{
    using ServiceStack;

    [Route("/admin/products/{Id}.json", "GET")]
    public class GetProduct : IReturn<ProductResponse>
    {
        public long Id { get; set; }
    }
}
