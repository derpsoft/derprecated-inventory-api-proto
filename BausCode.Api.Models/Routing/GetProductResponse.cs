using System.Collections.Generic;

namespace BausCode.Api.Models.Routing
{
    public class GetProductResponse
    {
        public GetProductResponse()
        {
            Product = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Product { get; set; }
    }
}