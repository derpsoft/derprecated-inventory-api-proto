namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses", "POST")]
    [Authenticate]
    public class CreateWarehouse : IReturn<CreateWarehouseResponse>
    {
    }
}
