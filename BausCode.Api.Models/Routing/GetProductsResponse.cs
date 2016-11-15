using System.Collections.Generic;

namespace BausCode.Api.Models.Routing
{
    public class GetProductsResponse
    {
        public List<Product> Products { get; set; } 
        public long Count { get; set; }
    }
}