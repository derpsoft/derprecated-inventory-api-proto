namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/{Id}", "DELETE")]
    [Authenticate]
    public class DeleteProduct : IReturn<DeleteProductResponse>
    {
        public int Id { get; set; }
    }
}
