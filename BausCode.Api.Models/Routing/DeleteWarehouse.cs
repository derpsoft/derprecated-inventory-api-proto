namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses/{Id}", "DELETE")]
    public class DeleteWarehouse : IReturn<DeleteWarehouseResponse>
    {
    }
}
