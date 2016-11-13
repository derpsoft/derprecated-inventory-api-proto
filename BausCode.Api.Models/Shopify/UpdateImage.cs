using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{ProductId}/images/{Id}.json", "PUT")]
    [DataContract]
    public class UpdateImage : IReturn<ImageResponse>
    {
        public long ProductId { get; set; }
        public long Id { get; set; }

        [DataMember(Name = "image")]
        public Image Image { get; set; }
    }
}