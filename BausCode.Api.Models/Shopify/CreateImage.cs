using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{ProductId}/images.json", "POST")]
    [DataContract]
    public class CreateImage : IReturn<ImageResponse>
    {
        public long ProductId { get; set; }

        [DataMember(Name = "image")]
        public Image Image { get; set; }
    }
}