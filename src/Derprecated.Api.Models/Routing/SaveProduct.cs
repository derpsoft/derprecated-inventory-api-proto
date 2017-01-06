namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/save", "POST, PUT")]
    [Authenticate]
    [RequiredRole(ApplyTo.All, Roles.Admin)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Put, Permissions.CanDoEverything, Permissions.CanManageProducts,
        Permissions.CanUpsertProducts)]
    public class SaveProduct : IReturn<SaveProductResponse>
    {
        public Product Product { get; set; }
    }
}
