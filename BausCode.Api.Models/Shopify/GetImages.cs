using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{ProductId}/images.json", "GET")]
    public class GetImages : IReturn<ImagesResponse>
    {
        public long ProductId { get; set; }
    }
}