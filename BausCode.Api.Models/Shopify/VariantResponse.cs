using System.Runtime.Serialization;

namespace BausCode.Api.Models.Shopify
{
    [DataContract]
    public class VariantResponse
    {
        [DataMember(Name = "variant")]
        public Variant Variant { get; set; }
    }
}