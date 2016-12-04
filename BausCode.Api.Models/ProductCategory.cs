namespace BausCode.Api.Models
{
    using System;

    public class ProductCategory : IAuditable
    {
        public int CategoryId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ProductId { get; set; }
        public ulong RowVersion { get; set; }
    }
}
