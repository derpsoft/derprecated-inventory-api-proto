namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses", "POST")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanUpsertWarehouses
        )]
    public class CreateWarehouse : IReturn<WarehouseResponse>
    {
        public Warehouse Warehouse { get; set; }
    }
}
