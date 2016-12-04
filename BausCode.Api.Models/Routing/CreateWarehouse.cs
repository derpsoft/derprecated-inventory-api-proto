namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses", "POST")]
    public class CreateWarehouse : IReturn<CreateWarehouseResponse>
    {
    }
}
