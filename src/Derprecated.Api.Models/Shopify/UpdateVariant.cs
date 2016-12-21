namespace Derprecated.Api.Models.Shopify
{
    using System.Runtime.Serialization;
    using ServiceStack;

    [Route("/admin/variants/{Id}.json", "PUT")]
    [DataContract]
    public class UpdateVariant : IReturn<VariantResponse>
    {
        [DataMember(IsRequired = true)]
        public long Id { get; set; }

        [DataMember(Name = "variant", IsRequired = true)]
        public Variant Variant { get; set; }
    }
}
