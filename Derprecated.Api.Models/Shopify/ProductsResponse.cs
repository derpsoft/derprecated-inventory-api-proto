namespace Derprecated.Api.Models.Shopify
{
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

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
