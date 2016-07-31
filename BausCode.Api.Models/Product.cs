using System;

namespace BausCode.Api.Models
{
    public class Product : IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}