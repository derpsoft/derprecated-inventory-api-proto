namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/warehouses/typeahead", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanReadWarehouses)]
    public class WarehouseTypeahead : IReturn<WarehousesResponse>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }
}
