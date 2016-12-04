namespace BausCode.Api.Models.Routing
{
    using System.Collections.Generic;

    public class GetProductQuantityOnHandResponse
    {
        public Dictionary<int, decimal> Quantity { get; set; }
    }
}
