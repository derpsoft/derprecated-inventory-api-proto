using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/variants/{Id}.json", "PUT")]
    [DataContract]
    public class UpdateVariant : IReturn<VariantResponse>
    {
        public long Id { get; set; }

        [DataMember(Name = "variant", IsRequired = true)]
        public Variant Variant { get; set; }
    }
}