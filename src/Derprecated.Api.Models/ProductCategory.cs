namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class ProductCategory : IAuditable
    {
        [ForeignKey(typeof (Category), OnDelete = "NO ACTION", OnUpdate = "NO ACTION")]
        public int CategoryId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

        [ForeignKey(typeof (Product), OnDelete = "NO ACTION", OnUpdate = "NO ACTION")]
        public int ProductId { get; set; }

        public ulong RowVersion { get; set; }
    }
}
