namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class InventoryTransaction : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime ModifyDate { get; set; }

        [References(typeof(Product))]
        public int ProductId { get; set; }
        [Reference]
        public Product Product { get; set; }

        public decimal Quantity { get; set; }
        public ulong RowVersion { get; set; }
        public InventoryTransactionTypes TransactionType { get; set; }

        [Required]
        public int UnitOfMeasureId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
