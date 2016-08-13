using System.Collections.Generic;

namespace BausCode.Api.Models.Routing
{
    public class GetProductQuantityOnHandResponse
    {
        public Dictionary<int, decimal> Quantity { get; set; }
    }
}