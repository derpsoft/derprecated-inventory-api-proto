namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using ServiceStack;

    public class VendorsResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<Dto.Vendor> Vendors { get; set; }
    }
}
