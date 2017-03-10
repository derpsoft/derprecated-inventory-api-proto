namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    public class Customer : IPrimaryKeyable, IAuditable
    {
        public int Id { get; set; }

        [Reference]
        public List<Order> Orders { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}
