using System;
using System.Collections.Generic;
using System.Linq;

namespace BausCode.Api.Models.Dto
{
    public class Product
    {
        public int Id { get; set; }
        public List<string> Fields { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ulong Version { get; set; }

        public static Product From(Models.Product source)
        {
            return new Product
            {
                Id = source.Id,
                Fields = source.Fields,
                CreatedAt = source.CreateDate,
                UpdatedAt =  source.ModifyDate,
                Version = source.RowVersion,
            };
        }
    }
}