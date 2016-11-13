using System.Runtime.Serialization;

namespace BausCode.Api.Models.Shopify
{
    [DataContract]
    internal class ProductResponse
    {
        [DataMember(Name = "product")]
        public Product Product { get; set; }
    }
}