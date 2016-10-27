using System.Collections.Generic;

namespace BausCode.Api.Models.Routing
{
    public class GetProductsResponse
    {
        public List<Dto.Product> Products { get; set; } 
        public long Count { get; set; }
    }
}