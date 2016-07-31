using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/warehouses/{Id}", "DELETE")]
    public class DeleteWarehouse : IReturn<DeleteWarehouseResponse>
    {
    }
}