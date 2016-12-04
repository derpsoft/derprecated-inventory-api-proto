namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/save", "POST")]
    [Authenticate]
    public class SaveProduct : IReturn<SaveProductResponse>
    {
        public Product Product { get; set; }
    }
}
