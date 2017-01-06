namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Location : IAuditable
    {
        public string Bin { get; set; }
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Rack { get; set; }
        public ulong RowVersion { get; set; }
        public string Shelf { get; set; }

        [ForeignKey(typeof (Warehouse), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int WarehouseId { get; set; }
    }
}
