namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/vendors/{Id}", "GET")]
    [Authenticate]
    public class GetVendor : IReturn<VendorResponse>
    {
        public int Id { get; set; }
    }
}
