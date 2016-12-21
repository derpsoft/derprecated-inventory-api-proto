namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses/{Id}", "DELETE")]
    [Authenticate]
    public class DeleteWarehouse : IReturn<DeleteWarehouseResponse>
    {
    }
}
