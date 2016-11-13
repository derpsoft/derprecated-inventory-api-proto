using System;
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

        /// <summary>
        ///     Merge fields from source into this.
        /// </summary>
        /// <param name="source"></param>
        public void Merge(Shopify.Image source)
        {
            SourceUrl = source.Url;
        }

        public static ProductImage From(Shopify.Image source)
        {
            var dest = new ProductImage() { ShopifyId = source.Id };

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