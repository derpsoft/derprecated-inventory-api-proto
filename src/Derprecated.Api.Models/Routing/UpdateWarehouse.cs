namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses/{Id}", "PUT, PATCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanUpsertWarehouses)]
    public class UpdateWarehouse : IReturn<WarehouseResponse>
    {
        public int Id { get; set; }
        public Dto.Warehouse Warehouse { get; set; }
    }
}
