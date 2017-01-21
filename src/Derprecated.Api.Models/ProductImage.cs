namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;
    using Shopify;

    public class ProductImage : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime ModifyDate { get; set; }

        [ForeignKey(typeof (Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

        public ulong RowVersion { get; set; }

        public long ShopifyId { get; set; }

        public string SourceUrl { get; set; }


        public static ProductImage From(Image source)
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
