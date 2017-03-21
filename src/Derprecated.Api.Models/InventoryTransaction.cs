namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class InventoryTransaction : IAuditable, IPrimaryKeyable
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
        [StringLength(16)]
        public InventoryTransactionTypes TransactionType { get; set; }

        [Required]
        [StringLength(64)]
        [Index(Unique=false)]
        public string UserAuthId { get; set; }
    }
}
