namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;

    public class GetProductsResponse
    {
        public long Count { get; set; }
        public List<Dto.Product> Products { get; set; }
    }
}
