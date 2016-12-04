namespace BausCode.Api.Models.Shopify
{
    using System.Runtime.Serialization;
    using ServiceStack;

    [Route("/admin/products/{ProductId}/images/{Id}.json", "PUT")]
    [DataContract]
    public class UpdateImage : IReturn<ImageResponse>
    {
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public long Id { get; set; }

        [DataMember(Name = "image", EmitDefaultValue = false)]
        public Image Image { get; set; }

        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public long ProductId { get; set; }
    }
}
