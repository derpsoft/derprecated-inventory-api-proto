namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using Models.Attributes;
    using ServiceStack;
    using ServiceStack.OrmLite;

    public class ProductHandler
    {
        public ProductHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Product Get(int id)
        {
            id.ThrowIfLessThan(1);
            var product = Db.Single<Product>(new {Id = id, IsDeleted = false});
            Db.LoadReferences(product);
            return product;
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
        public List<Product> List(int skip = 0, int take = 25)
        {
            return Db.LoadSelect(
                Db.From<Product>()
                  .Where(x => !x.IsDeleted)
                  .Skip(skip)
                  .Take(take)
                );
        }

        public Product Save(Product product)
        {
            product.ThrowIfNull(nameof(product));

            if (product.Id >= 1)
            {
                var existing = Get(product.Id);
                // ReSharper disable once PossibleUnintendedReferenceComparison
                if (default(Product) == existing)
                    throw new ArgumentException("invalid Id for existing product", nameof(product));

                product = existing.PopulateFromPropertiesWithAttribute(product, typeof (WhitelistAttribute));
            }
            Db.Save(product);

            return product;
        }

        //        /// <summary>
        //        ///     Update an existing Product.
        //        /// </summary>
        //        /// <param name="id">The ID of the Product to update.</param>
        //        /// <param name="updatedProduct">The values to update the existing Product with.</param>
        //        /// <returns></returns>
        //        public Product Update(int id, UpdateProduct updatedProduct)
        //        {
        //            updatedProduct.ThrowIfNull();
        //
        //            var product = Get(id);
        //            product.ThrowIfNull();
        //
        //            product = product
        //                .PopulateFromPropertiesWithAttribute(updatedProduct, typeof (WhitelistAttribute));
        //
        //            Db.Save(product, true);
        //
        //            return product;
        //        }

        public Product Update<T>(int id, IUpdatableField<T> update)
        {
            update.ThrowIfNull();
            var product = Get(id);
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

            Db.UpdateOnly(new Product {ShopifyId = shopifyId},
                q.Update(x => x.ShopifyId).Where(x => x.Id == productId));
        }

        public Product Delete(int id)
        {
            var existing = Get(id);
            if (default(Product) == existing)
                throw new ArgumentException("unable to locate product with id");
            return Db.SoftDelete(existing);
        }
    }
}
