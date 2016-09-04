﻿using System.Collections.Generic;
using ServiceStack;

// ReSharper disable UnusedMember.Global

namespace BausCode.Api.Models.Dto
{
    public class Product
    {
        public int Id { get; set; }
        public ulong Version { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }

        public List<Variant> Variants { get; set; }

        public static Product From(Models.Product source)
        {
            return new Product
            {
                Id = source.Id,
                Version = source.RowVersion,
                Variants = source.Variants.Map(Variant.From)
            }.PopulateWith(source.Meta ?? new ProductMeta());
        }
    }
}