namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Image : IPrimaryKeyable, IAuditable, ISoftDeletable
    {
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
        public ulong RowVersion { get; set; }

        [StringLength(2000)]
        public string SourceUrl { get; set; }

        [StringLength(2000)]
        public string Url { get; set; }
    }
}
