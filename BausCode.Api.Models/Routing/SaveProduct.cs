namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/save", "POST, PUT")]
    [Authenticate]
    [RequiredRole(ApplyTo.All, Roles.Admin)]
    [RequiresAnyPermission(ApplyTo.Post, Permissions.CanDoEverything, Permissions.CanSaveProducts, Permissions.CanCreateProducts)]
    [RequiresAnyPermission(ApplyTo.Put, Permissions.CanDoEverything, Permissions.CanSaveProducts, Permissions.CanUpdateProducts)]
    public class SaveProduct : IReturn<SaveProductResponse>
    {
        public Product Product { get; set; }
    }
}
