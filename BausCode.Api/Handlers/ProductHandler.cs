using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BausCode.Api.Models;
using BausCode.Api.Models.Attributes;
using BausCode.Api.Models.Routing;
using ServiceStack;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    internal class ProductHandler
    {
        public ProductHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Product GetProduct(int id)
        {
            id.ThrowIfLessThan(1);
            return Db.LoadSingleById<Product>(id);
        }

        /// <summary>
        ///     Get all products.
        /// </summary>
        /// <returns>
        ///     A list of Product elements.
        /// </returns>
        /// <remarks>
        ///     Might be slow, use with caution.
        /// </remarks>
        public List<Product> GetProducts(int? skip, int? take)
        {
            return Db.LoadSelect(Db.From<Product>().Skip(skip).Take(take));
        }

        /// <summary>
        ///     Get quantity on hand for a particular Variant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        ///     Wraps VariantHandler#GetQuantityOnHand(1)
        /// </remarks>
        public Dictionary<int, decimal> GetQuantityOnHand(int productId)
        {
            var product = GetProduct(productId);

            return new ProductHandler(Db, User).GetQuantityOnHand(productId);
        }

        public Product Save(Product product)
        {
            product.ThrowIfNull(nameof(product));

            if (product.Id >= 1)
            {
                var existing = GetProduct(product.Id);
                if (default(Product) == existing)
                    throw new ArgumentException("invalid Id for existing product", nameof(product));

                var upsert = existing.PopulateWith(product);
                Db.Save(upsert, true);
            }
            return product;
        }

        /// <summary>
        ///     Update an existing Product.
        /// </summary>
        /// <param name="id">The ID of the Product to update.</param>
        /// <param name="updatedProduct">The values to update the existing Product with.</param>
        /// <returns></returns>
        public Product Update(int id, UpdateProduct updatedProduct)
        {
            updatedProduct.ThrowIfNull();

            var product = GetProduct(id);
            product.ThrowIfNull();

            product = product
                .PopulateFromPropertiesWithAttribute(updatedProduct, typeof (WhitelistAttribute));

            Db.Save(product, true);

            return product;
        }

        public Product Update<T>(int id, IUpdatableField<T> update)
        {
            update.ThrowIfNull();
            var product = GetProduct(id);
            product.SetProperty(update.FieldName, update.Value);
            Db.UpdateOnly(product, new[] {update.FieldName}, p => p.Id == product.Id);
            return product;
        }

        public long Count()
        {
            return Db.Count<Product>();
        }

        public void SetShopifyId(int productId, long shopifyId)
        {
            var q = Db.From<Product>();

            Db.UpdateOnly(new Product() {ShopifyId = shopifyId},
                q.Update(x => x.ShopifyId).Where(x => x.Id == productId));
        }

        public Product Create(CreateProduct createProduct)
        {
            throw new NotImplementedException();
        }
    }
}