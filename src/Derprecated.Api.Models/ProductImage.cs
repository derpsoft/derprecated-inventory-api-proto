namespace Derprecated.Api.Models
{
    using System;
    using Attributes;
    using ServiceStack.DataAnnotations;

    public sealed class ProductImage : IAuditable, ISoftDeletable, IPrimaryKeyable
    {
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Reference]
        public Image Image { get; set; }

        [ForeignKey(typeof(Image), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        [Whitelist]
        public int ImageId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime ModifyDate { get; set; }

        [Reference]
        public Product Product { get; set; }

        [ForeignKey(typeof(Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        [Whitelist]
        public int ProductId { get; set; }

        public ulong RowVersion { get; set; }
    }
}
