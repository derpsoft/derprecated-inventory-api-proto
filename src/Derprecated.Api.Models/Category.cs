namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    public class Category : IAuditable, ISoftDeletable, IPrimaryKeyable
    {
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime ModifyDate { get; set; }
        public string Name { get; set; }

        [Reference]
        public Category Parent { get; set; }

        [ForeignKey(typeof(Category), OnUpdate = "NO ACTION", OnDelete = "NO ACTION")]
        public int? ParentId { get; set; }

        [Reference]
        public List<Product> Products { get; set; }

        public ulong RowVersion { get; set; }
        public int Sort { get; set; }
    }
}
