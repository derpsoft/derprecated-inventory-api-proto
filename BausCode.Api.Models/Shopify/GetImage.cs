using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{ProductId}/images/{Id}.json", "GET")]
    public class GetImage : IReturn<ImageResponse>
    {
        public long ProductId { get; set; }
        public long Id { get; set; }
    }
}