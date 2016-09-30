using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Routing.Shopify
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GetProductsResponse
    {
        [Alias("products")]
        public List<Dto.Shopify.Product> Products { get; set; }
    }
}