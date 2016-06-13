using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}", "GET")]
    public class GetProduct : IReturn<GetProductResponse>
    {
        public int Id { get; set; }
    }
}