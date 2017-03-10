namespace Derprecated.Api.Models
{
    using System;

    public class Merchant : IPrimaryKeyable, IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}
