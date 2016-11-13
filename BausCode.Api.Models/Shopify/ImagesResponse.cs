using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Shopify
{
    public class ImagesResponse
    {
        [Alias("images")]
        public List<Image> Images { get; set; }
    }
}