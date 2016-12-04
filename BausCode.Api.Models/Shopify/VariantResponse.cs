namespace BausCode.Api.Models.Shopify
{
    using System.Runtime.Serialization;

    [DataContract]
    public class VariantResponse
    {
        [DataMember(Name = "variant")]
        public Variant Variant { get; set; }
    }
}
