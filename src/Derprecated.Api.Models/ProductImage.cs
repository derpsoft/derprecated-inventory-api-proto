namespace Derprecated.Api.Models
{
    using System;
    using Attributes;
    using ServiceStack.DataAnnotations;
    using Shopify;

    public class ProductImage : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime ModifyDate { get; set; }

        [ForeignKey(typeof (Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        [Whitelist]
        public int ProductId { get; set; }

        public ulong RowVersion { get; set; }

        [Whitelist]
        public long ShopifyId { get; set; }

        [Whitelist]
        public string SourceUrl { get; set; }
    }
}
