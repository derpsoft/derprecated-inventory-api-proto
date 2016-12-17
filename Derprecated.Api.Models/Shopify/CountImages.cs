namespace Derprecated.Api.Models.Shopify
{
    using ServiceStack;

    [Route("/admin/products/{ProductId}/images/count.json", "GET")]
    public class CountImages : IReturn<CountResponse>
    {
        public long ProductId { get; set; }
    }
}
