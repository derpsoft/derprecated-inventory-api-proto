namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/{Id}", "DELETE")]
    public class DeleteProduct : IReturn<DeleteProductResponse>
    {
    }
}
