namespace BausCode.Api.Models.Routing
{
    using System.Collections.Generic;

    public class GetProductsResponse
    {
        public long Count { get; set; }
        public List<Product> Products { get; set; }
    }
}
