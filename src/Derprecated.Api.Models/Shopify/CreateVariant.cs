namespace Derprecated.Api.Models.Shopify
{
    using System.Runtime.Serialization;
    using ServiceStack;

    [Route("/admin/products/{ProductId}/variants.json", "POST")]
    [DataContract]
    public class CreateVariant : IReturn<VariantResponse>
    {
        public long ProductId { get; set; }

        [DataMember(Name = "variant", IsRequired = true)]
        public Variant Variant { get; set; }
    }
}
