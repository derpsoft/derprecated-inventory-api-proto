using System;

namespace BausCode.Api.Models
{
    public class ProductCategory : IAuditable
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}