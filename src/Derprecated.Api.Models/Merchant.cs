namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Merchant : IPrimaryKeyable, IAuditable
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}
