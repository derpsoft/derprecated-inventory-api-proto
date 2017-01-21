namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses/{Id}", "DELETE")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanDeleteWarehouses
        )]
    public class DeleteWarehouse : IReturn<DeleteWarehouseResponse>
    {
        public int Id { get; set; }
    }
}
