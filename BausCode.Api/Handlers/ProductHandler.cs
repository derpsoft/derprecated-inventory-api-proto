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

        public Product GetProductVariant(int productId, int variantId)
        {
            productId.ThrowIfLessThan(1);
            variantId.ThrowIfLessThan(1);

            return Db.Select(
                Db.From<Product>().Join<Variant>()
                    .Where(p => p.Id == productId)
                    .And<Variant>(v => v.Id == variantId)
                    .Limit(1)
                ).Single();
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

            var variantHandler = new VariantHandler(Db, User);

            return variantHandler.QuantityOnHand(product.Variants.Map(v => v.Id));
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

        public List<Product> Search(string query, int? skip, int? take)
        {
            return Db.Select(
                Db.From<Product>()
                    .UnsafeWhere("FREETEXT(Description, {0})", query)
                    .Skip(skip)
                    .Take(take)
                );
        }
    }
}