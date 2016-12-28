namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using Dto;
    using ServiceStack;

    public class InventoryTransactionsResponse
    {
        public List<InventoryTransaction> InventoryTransactions { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
