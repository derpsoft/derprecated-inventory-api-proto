namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/{Id}", "DELETE")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanDeleteProducts)]
    public class DeleteProduct : IReturn<DeleteProductResponse>
    {
        public int Id { get; set; }
    }
}
