namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanReadWarehouses)]
    public class GetWarehouses : IReturn<WarehousesResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }
}
