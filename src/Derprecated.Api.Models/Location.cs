namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Location : IAuditable, ISoftDeletable, IPrimaryKeyable
    {
        [StringLength(10)]
        public string Bin { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime ModifyDate { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Rack { get; set; }

        public ulong RowVersion { get; set; }

        [StringLength(10)]
        public string Shelf { get; set; }

        [ForeignKey(typeof (Warehouse), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int WarehouseId { get; set; }
    }
}
