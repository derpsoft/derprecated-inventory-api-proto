namespace BausCode.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class VendorResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public Vendor Vendor { get; set; }
    }
}
