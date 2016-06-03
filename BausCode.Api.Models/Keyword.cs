using System;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class Keyword : IAuditable
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof (UserAuth))]
        public int UserAuthId { get; set; }

        public string Value { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}