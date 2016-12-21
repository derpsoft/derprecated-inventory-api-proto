namespace Derprecated.Api.Models.Shopify
{
    using System.Runtime.Serialization;
    using ServiceStack;

    [Route("/admin/products/{ProductId}/images.json", "POST")]
    [DataContract]
    public class CreateImage : IReturn<ImageResponse>
    {
        [DataMember(Name = "image")]
        public Image Image { get; set; }

        public long ProductId { get; set; }
    }
}
