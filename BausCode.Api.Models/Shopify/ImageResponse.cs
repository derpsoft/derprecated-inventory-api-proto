using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Shopify
{
    public class ImageResponse
    {
        [Alias("image")]
        public Image Image { get; set; }
    }
}