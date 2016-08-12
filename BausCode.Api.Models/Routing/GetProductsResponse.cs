using System.Collections.Generic;

namespace BausCode.Api.Models.Routing
{
    public class GetProductsResponse
    {
        public List<Dictionary<string, object>> Products { get; set; } 
    }
}