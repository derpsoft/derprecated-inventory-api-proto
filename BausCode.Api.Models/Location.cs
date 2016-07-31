using System;

namespace BausCode.Api.Models
{
    public class Location : IAuditable
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public int Rack { get; set; }
        public int Shelf { get; set; }
        public int Bin { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}