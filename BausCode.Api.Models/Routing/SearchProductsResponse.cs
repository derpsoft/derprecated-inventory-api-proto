using System.Collections.Generic;

namespace BausCode.Api.Models.Routing
{
    public class SearchProductsResponse
    {
        public string Query { get; set; }
        public int Count { get; set; }
        public List<Dto.Product> Products { get; set; } 
    }
}