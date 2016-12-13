namespace BausCode.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Vendor : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Name { get; set; }
        public ulong RowVersion { get; set; }
    }
}
