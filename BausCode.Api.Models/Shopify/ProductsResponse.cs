using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Shopify
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ProductsResponse
    {
        public ProductsResponse()
        {
            Products = new List<Product>();
        }

        [Alias("products")]
        public List<Product> Products { get; set; }
    }
}