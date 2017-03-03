namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Attributes;
    using ServiceStack.DataAnnotations;

    public class Product : IAuditable, ISoftDeletable
    {
        public Product()
        {
            ProductImages = new List<ProductImage>();
        }

        [Whitelist]
        [EqualityCheck]
        public string Barcode { get; set; }

        [Reference]
        public Category Category { get; set; }

        [Whitelist]
        [ForeignKey(typeof(Category), OnDelete = "NO ACTION", OnUpdate = "CASCADE")]
        public int? CategoryId { get; set; }

        [Whitelist]
        [EqualityCheck]
        public string Color { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [Whitelist]
        [EqualityCheck]
        public string Description { get; set; }

        [Whitelist]
        [EqualityCheck]
        public int Grams { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        [EqualityCheck]
        public int Id { get; set; }

        [Reference]
        public List<ProductImage> ProductImages { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime ModifyDate { get; set; }

        [Whitelist]
        [EqualityCheck]
        public decimal Price { get; set; }

        public ulong RowVersion { get; set; }

        [Whitelist]
        [EqualityCheck]
        public long? ShopifyId { get; set; }

        [EqualityCheck]
        public long? ShopifyVariantId { get; set; }

        [Whitelist]
        [EqualityCheck]
        [Index(Unique = true)]
        [StringLength(200)]
        public string Sku { get; set; }

        [Whitelist]
        [EqualityCheck]
        public string Tags { get; set; }

        [Whitelist]
        [EqualityCheck]
        public string Title { get; set; }

        [Whitelist]
        [EqualityCheck]
        public string Vendor { get; set; }

        [Whitelist]
        [EqualityCheck]
        public decimal Weight { get; set; }

        /// <summary>
        ///     Acceptable values are one of: lb, kg, oz, g
        /// </summary>
        [Whitelist]
        [EqualityCheck]
        public string WeightUnit { get; set; }


        /// <summary>
        ///     Merge fields from source into this.
        /// </summary>
        /// <param name="source"></param>
        public void Merge(Shopify.Product source)
        {
            Title = source.Title;
            Description = source.BodyHtml;
            Tags = source.Tags;
        }

        public void Merge(Product source)
        {
            Title = source.Title;
            Description = source.Description;
            Tags = source.Tags;
        }

        public static Product From(Shopify.Product source)
        {
            var dest = new Product {ShopifyId = source.Id};

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
            Sku = Sku.ToUpper();
        }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            if (product != null)
                return this.DeepEquals(product);

            return false;
        }
    }
}
