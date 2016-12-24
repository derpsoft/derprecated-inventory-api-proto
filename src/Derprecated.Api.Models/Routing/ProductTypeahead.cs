namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/products/typeahead", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    public class ProductTypeahead : IReturn<ProductsResponse>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }
}
