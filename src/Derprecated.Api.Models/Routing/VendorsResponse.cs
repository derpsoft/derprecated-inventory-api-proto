namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using Dto;
    using ServiceStack;

    public class VendorsResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<Vendor> Vendors { get; set; }
    }
}
