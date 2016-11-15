using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products.json", "POST")]
    [DataContract]
    public class CreateProduct : IReturn<ProductResponse>
    {
        [DataMember(Name = "product")]
        public Product Product { get; set; }
    }
}