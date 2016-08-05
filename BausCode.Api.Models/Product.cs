using ServiceStack.DataAnnotations;
using System;

namespace BausCode.Api.Models
{
    public class Product : IAuditable
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public long ShopifyId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}