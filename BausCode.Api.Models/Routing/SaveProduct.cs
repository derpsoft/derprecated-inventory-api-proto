using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/save", "POST")]
    public class SaveProduct : IReturn<SaveProductResponse>
    {
        public Product Product { get; set; }
    }
}