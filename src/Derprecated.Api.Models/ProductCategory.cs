namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class ProductCategory : IAuditable
    {
        [ForeignKey(typeof (Category), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int CategoryId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

        [ForeignKey(typeof (Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

        public ulong RowVersion { get; set; }
    }
}
