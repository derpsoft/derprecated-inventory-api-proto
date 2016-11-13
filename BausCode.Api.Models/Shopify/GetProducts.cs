using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products.json", "GET")]
    [DataContract]
    public class GetProducts : IReturn<ProductsResponse>
    {
        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}