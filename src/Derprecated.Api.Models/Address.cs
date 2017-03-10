namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    public class Address : IAuditable, ISoftDeletable, IPrimaryKeyable
    {
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsPrimary { get; set; }

        public DateTime ModifyDate { get; set; }

        [Index(Unique = false)]
        [StringLength(256)]
        public string UserId { get; set; }

        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Line1 { get; set; }

        [StringLength(256)]
        public string Line2 { get; set; }

        [StringLength(256)]
        public string City { get; set; }

        [StringLength(256)]
        public string State { get; set; }

        [StringLength(16)]
        public string Zip { get; set; }

        public ulong RowVersion { get; set; }
    }
}
