using System;

namespace BausCode.Api.Models
{
    public class ProductMeta : IAuditable
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string MetaName { get; set; }
        public string MetaValue { get; set; }
        public string MetaType { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}