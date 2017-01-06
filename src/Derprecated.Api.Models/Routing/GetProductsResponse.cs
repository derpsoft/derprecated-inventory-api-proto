namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using Dto;

    public class GetProductsResponse
    {
        public long Count { get; set; }
        public List<Product> Products { get; set; }
    }
}
