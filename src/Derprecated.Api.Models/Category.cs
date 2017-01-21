namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    public class Category : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime ModifyDate { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        [Reference]
        public List<Product> Products { get; set; }

        public ulong RowVersion { get; set; }
        public int Sort { get; set; }
    }
}
