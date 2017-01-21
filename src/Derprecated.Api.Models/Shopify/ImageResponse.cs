namespace Derprecated.Api.Models.Shopify
{
    using ServiceStack.DataAnnotations;

    public class ImageResponse
    {
        [Alias("image")]
        public Image Image { get; set; }
    }
}
