using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{ProductId}/variants.json", "POST")]
    [DataContract]
    public class CreateVariant : IReturn<VariantResponse>
    {
        public long ProductId { get; set; }

        [DataMember(Name = "variant", IsRequired = true)]
        public Variant Variant { get; set; }
    }
}