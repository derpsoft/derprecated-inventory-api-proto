namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class InventoryTransactionResponse
    {
        public InventoryTransaction InventoryTransaction { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
