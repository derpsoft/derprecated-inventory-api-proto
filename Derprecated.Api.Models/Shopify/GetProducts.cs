namespace Derprecated.Api.Models.Shopify
{
    using System.Runtime.Serialization;
    using ServiceStack;

    [Route("/admin/products.json", "GET")]
    [DataContract]
    public class GetProducts : IReturn<ProductsResponse>
    {
        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
