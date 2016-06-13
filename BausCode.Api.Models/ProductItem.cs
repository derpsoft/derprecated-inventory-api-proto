using System;

namespace BausCode.Api.Models
{
    public class ProductItem :IAuditable
    {
        public int ProductId { get; set; }
        public int ItemId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}