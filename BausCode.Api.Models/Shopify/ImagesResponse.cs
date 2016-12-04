namespace BausCode.Api.Models.Shopify
{
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    public class ImagesResponse
    {
        [Alias("images")]
        public List<Image> Images { get; set; }
    }
}
