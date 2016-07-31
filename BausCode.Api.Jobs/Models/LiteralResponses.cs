using System;
using BausCode.Api.Models;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Jobs.Models
{
    public class LiteralResponses : IAuditable
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Index(Unique = true)]
        public string Response { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}