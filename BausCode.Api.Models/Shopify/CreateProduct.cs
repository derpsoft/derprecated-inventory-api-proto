namespace BausCode.Api.Models.Shopify
{
    using System.Runtime.Serialization;
    using ServiceStack;

    [Route("/admin/products.json", "POST")]
    [DataContract]
    public sealed class CreateProduct : IReturn<CreateProductResponse>
    {
        [DataMember(Name = "product")]
        public Product Product { get; set; }
    }
}
