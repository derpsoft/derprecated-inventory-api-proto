namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    public class Warehouse : IAuditable
    {
        public Warehouse()
        {
            Locations = new List<Location>();
        }

        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Reference]
        public List<Location> Locations { get; set; }

        public DateTime ModifyDate { get; set; }
        public string Name { get; set; }
        public ulong RowVersion { get; set; }
    }
}
