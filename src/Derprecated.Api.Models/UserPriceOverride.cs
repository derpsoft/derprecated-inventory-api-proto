namespace Derprecated.Api.Models
{
    using System;
    using Attributes;
    using ServiceStack.Auth;
    using ServiceStack.DataAnnotations;

    public class UserPriceOverride : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime ModifyDate { get; set; }

        [Whitelist]
        public decimal Price { get; set; }

        [ForeignKey(typeof (Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

        public ulong RowVersion { get; set; }

        [ForeignKey(typeof (UserAuth), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int UserAuthId { get; set; }
    }
}
