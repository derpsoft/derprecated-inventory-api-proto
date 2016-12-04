namespace BausCode.Api.Models
{
    using System;

    public class Order : IAuditable
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
        public int UserId { get; set; }
    }
}
