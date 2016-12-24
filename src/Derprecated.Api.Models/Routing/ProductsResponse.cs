namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using Dto;
    using ServiceStack;

    public class ProductsResponse
    {
        public List<Product> Products { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
