using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}", "DELETE")]
    public class DeleteProduct : IReturn<DeleteProductResponse>
    {
    }
}