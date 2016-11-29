﻿using System;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class ProductImage : IAuditable
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public long ShopifyId { get; set; }

        [ForeignKey(typeof (Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }

        public string SourceUrl { get; set; }

        
        public static ProductImage From(Shopify.Image source)
        {
            var dest = new ProductImage
            {
                ShopifyId = source.Id.GetValueOrDefault(),
                SourceUrl = source.Url
            };


            return dest;
        }
        public static ProductImage From(ProductImage source)
        {
            var dest = new ProductImage
            {
                ShopifyId = source.Id,
                SourceUrl = source.SourceUrl
            };


            return dest;
        }

        public void OnInsert()
        {
            OnUpsert();
        }

        public void OnUpdate()
        {
            OnUpsert();
        }

        private void OnUpsert()
        {
        }
    }
}