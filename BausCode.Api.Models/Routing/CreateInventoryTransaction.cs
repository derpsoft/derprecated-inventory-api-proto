using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    /// <summary>
    /// Used for all inventory operations, including receiving, moving, selling, quantity changes, etc.
    /// </summary>
    [Route("/api/v1/inventory-transaction", "POST")]
    public class CreateInventoryTransaction : IReturn<CreateInventoryTransactionResponse>
    {
    }
}