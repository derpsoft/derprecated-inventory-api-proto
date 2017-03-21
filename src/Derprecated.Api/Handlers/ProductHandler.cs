namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using Models.Attributes;
    using ServiceStack;
    using ServiceStack.OrmLite;

    public class ProductHandler : IHandler<Product>
    {
        public ProductHandler(IDbConnection db)
        {
            Db = db;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Product Get(string sku, bool includeDeleted = false)
        {
            sku.ThrowIfNullOrEmpty();

            var query = Db.From<Product>()
                          .Where(x => x.Sku == sku.ToUpper());

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query)
                     .First();
        }

        public void SetShopifyId(int productId, long shopifyId)
        {
            var q = Db.From<Product>();

            Db.UpdateOnly(new Product {ShopifyId = shopifyId},
                q.Update(x => x.ShopifyId).Where(x => x.Id == productId));
        }

        public Product Restore(int id)
        {
            var existing = Get(id);
            if (default(Product) == existing)
                throw new ArgumentException("unable to locate product with id");
            if (existing.IsDeleted)
                throw new Exception("that product was already deleted");
            return Db.SoftDelete(existing);
        }

        public Product Get(int id, bool includeDeleted = false)
        {
            id.ThrowIfLessThan(1);

            var query = Db.From<Product>()
                          .Where(x => x.Id == id);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query)
                     .First();
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
        public List<Product> List(int skip = 0, int take = 25, bool includeDeleted = false)
        {
            skip.ThrowIfLessThan(0);
            take.ThrowIfLessThan(1);

            var query = Db.From<Product>()
                          .Skip(skip)
                          .Take(take);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query);
        }

        public List<Product> Typeahead(string q, bool includeDeleted = false)
        {
            return Db.LoadSelect(
                Db.From<Product>()
                  .Where(x => x.Title.Contains(q))
                  .Or(x => x.Sku.Contains(q.ToUpper()))
                  .SelectDistinct()
            );
        }

        public Product Save(Product product, bool includeReferences = false)
        {
            product.ThrowIfNull(nameof(product));

            if (product.Id >= 1)
            {
                var existing = Get(product.Id);
                // ReSharper disable once PossibleUnintendedReferenceComparison
                if (default(Product) == existing)
                    throw new ArgumentException("invalid Id for existing product", nameof(product));
                if (existing.IsDeleted)
                    throw new Exception("deleted products must be undeleted before updates can be made");

                product = existing.PopulateFromPropertiesWithAttribute(product, typeof(WhitelistAttribute));
            }
            Db.Save(product, includeReferences);

            return product;
        }

        public List<Product> SaveMany(List<Product> products)
        {
            Db.InsertAll(products);
            return products;
        }

        public long Count(bool includeDeleted = false)
        {
            if (includeDeleted)
                return Db.Count<Product>();

            return Db.Count<Product>(x => !x.IsDeleted);
        }

        public Product Delete(int id)
        {
            var existing = Get(id);
            if (default(Product) == existing)
                throw new ArgumentException("unable to locate product with id");
            if (existing.IsDeleted)
                throw new Exception("that product was already deleted");
            return Db.SoftDelete(existing);
        }
    }
}
