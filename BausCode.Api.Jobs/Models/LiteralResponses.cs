namespace BausCode.Api.Jobs.Models
{
    using System;
    using Api.Models;
    using ServiceStack.DataAnnotations;

    public class LiteralResponses : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime ModifyDate { get; set; }

        [Index(Unique = true)]
        public string Response { get; set; }

        public ulong RowVersion { get; set; }
    }
}
