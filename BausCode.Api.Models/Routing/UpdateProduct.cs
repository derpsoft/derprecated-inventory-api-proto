using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}", "PUT")]
    public class UpdateProduct : IReturn<UpdateProductResponse>
    {
    }
}