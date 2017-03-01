namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    public class Image : IPrimaryKeyable, IAuditable, ISoftDeletable
    {
        public Image()
        {
            ProductImages = new List<ProductImage>();
        }

        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [StringLength(10)]
        public string Extension { get; set; }

        [StringLength(100)]
        public string Filename { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(50)]
        public string MimeType { get; set; }

        public DateTime ModifyDate { get; set; }

        [Reference]
        public List<ProductImage> ProductImages { get; set; }

        [Ignore]
        public List<Product> Products { get; set; }

        public ulong RowVersion { get; set; }

        [StringLength(2000)]
        public string SourceUrl { get; set; }

        [StringLength(2000)]
        public string Url { get; set; }
    }
}
