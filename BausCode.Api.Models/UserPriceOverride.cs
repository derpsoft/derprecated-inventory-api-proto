using System;
using BausCode.Api.Models.Attributes;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class UserPriceOverride : IAuditable
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof (Variant), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int VariantId { get; set; }

        [ForeignKey(typeof (UserAuth), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int UserAuthId { get; set; }

        [Whitelist]
        public decimal Price { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}