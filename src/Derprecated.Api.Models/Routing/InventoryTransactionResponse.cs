namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    public class InventoryTransactionResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public Dto.InventoryTransaction InventoryTransaction { get; set; }
    }
}
