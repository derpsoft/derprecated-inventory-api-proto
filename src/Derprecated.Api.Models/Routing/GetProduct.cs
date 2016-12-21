namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/products/{Id}", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    public class GetProduct : IReturn<ProductResponse>
    {
        public List<string> Fields { get; set; }

        [Required]
        public int Id { get; set; }
    }
}
