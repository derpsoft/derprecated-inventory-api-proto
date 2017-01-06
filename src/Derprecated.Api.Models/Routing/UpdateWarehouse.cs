namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    [Route("/api/v1/warehouses/{Id}", "PUT, PATCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanUpsertWarehouses
        )]
    public class UpdateWarehouse : IReturn<WarehouseResponse>
    {
        public int Id { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
