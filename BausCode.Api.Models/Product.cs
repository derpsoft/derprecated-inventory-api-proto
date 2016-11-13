using System;
using System.Collections.Generic;
using System.Linq;
using BausCode.Api.Models.Attributes;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class Product : IAuditable
    {
        public Product()
        {
            Variants = new List<Variant>();
            Images = new List<ProductImage>();
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public long? ShopifyId { get; set; }

        [Whitelist]
        public string Title { get; set; }

        [Whitelist]
        public string Description { get; set; }

        [Whitelist]
        public string Tags { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }

        [Reference]
        public List<Variant> Variants { get; set; }

        [Reference]
        public List<ProductImage> Images { get; set; }


        /// <summary>
        ///     Merge fields from source into this.
        /// </summary>
        /// <param name="source"></param>
        public void Merge(Shopify.Product source)
        {
            Title = source.Title;
            Description = source.BodyHtml;
            Tags = source.Tags;

            foreach (var pv in source.Variants)
            {
                var v = Variants.FirstOrDefault(x => x.ShopifyId == pv.Id);

                if (default(Variant) == v)
                {
                    Variants.Add(Variant.From(pv));
                }
                else
                {
                    v.Merge(pv);
                }
            }

            foreach (var img in source.Images)
            {
                var i = Images.FirstOrDefault(x => x.ShopifyId == img.Id);

                if (default(ProductImage) == i)
                {
                    Images.Add(ProductImage.From(img));
                }
                else
                {
                    i.Merge(img);
                }
            }
        }

        public static Product From(Shopify.Product source)
        {
            var dest = new Product {ShopifyId = source.Id.Value};

            dest.Merge(source);

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