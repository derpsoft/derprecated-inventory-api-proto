namespace Derprecated.Api.Models.Shopify
{
    using ServiceStack;

    [Route("/admin/variants/{Id}.json", "GET")]
    public class GetVariant : IReturn<VariantResponse>
    {
        public long Id { get; set; }
    }
}
