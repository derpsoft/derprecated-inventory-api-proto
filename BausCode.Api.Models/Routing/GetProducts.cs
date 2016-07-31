using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products", "GET")]
    public class GetProducts : IReturn<GetProductsResponse>
    {
    }
}