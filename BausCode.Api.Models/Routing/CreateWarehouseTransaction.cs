using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    /// <summary>
    /// Used for all warehouse operations, including receiving, moving, selling, quantity changes, etc.
    /// </summary>
    [Route("/api/v1/warehouse-transaction", "POST")]
    public class CreateWarehouseTransaction : IReturn<CreateWarehouseTransactionResponse>
    {
    }
}