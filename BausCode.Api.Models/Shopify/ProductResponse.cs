using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [DataContract]
    public class ProductResponse
    {
        [DataMember(Name = "product")]
        public Product Product { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}