namespace Derprecated.Api.Models
{
    using System;

    public class Location : IAuditable
    {
        public int Bin { get; set; }

        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Rack { get; set; }
        public ulong RowVersion { get; set; }
        public int Shelf { get; set; }
        public int WarehouseId { get; set; }
    }
}
