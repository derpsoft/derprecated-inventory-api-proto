namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    /// <summary>
    ///     Used for all warehouse operations, including receiving, moving, selling, quantity changes, etc.
    /// </summary>
    [Route("/api/v1/warehouse-transaction", "POST")]
    [Authenticate]
    public class CreateWarehouseTransaction : IReturn<CreateWarehouseTransactionResponse>
    {
    }
}
