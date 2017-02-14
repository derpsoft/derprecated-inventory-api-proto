namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Vendor : IAuditable, ISoftDeletable
    {
        [StringLength(400)]
        public string ContactAddress { get; set; }

        [StringLength(256)]
        public string ContactEmail { get; set; }

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(20)]
        public string ContactPhone { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime ModifyDate { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public ulong RowVersion { get; set; }
    }
}
