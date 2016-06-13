using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/", "POST")]
    public class CreateProduct : IReturn<CreateProductResponse>
    {
    }
}