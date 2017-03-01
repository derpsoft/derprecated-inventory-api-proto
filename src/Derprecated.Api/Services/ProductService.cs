namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Handlers;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;

    public class ProductService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(ProductService));

        public object Any(ProductCount request)
        {
            var resp = new Dto<long>();
            var handler = new ProductHandler(Db, CurrentSession);

            resp.Result = handler.Count(request.IncludeDeleted);

            return resp;
        }

        public object Get(Product request)
        {
            var resp = new Dto<Product>();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            var product = Product.From(productHandler.Get(request.Id, request.IncludeDeleted));

            resp.Result = product;
            resp.Result.QuantityOnHand = inventoryHandler.GetQuantityOnHand(product.Id);

            return resp;
        }

        public object Get(ProductBySku request)
        {
            var resp = new Dto<Product>();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            var product = Product.From(productHandler.Get(request.Sku, request.IncludeDeleted));

            resp.Result = product;
            resp.Result.QuantityOnHand = inventoryHandler.GetQuantityOnHand(product.Id);

            return resp;
        }

        public object Any(Product request)
        {
            var resp = new Dto<Product>();
            var productHandler = new ProductHandler(Db, CurrentSession);

            var product = productHandler.Save(new Models.Product().PopulateWith(request));

            resp.Result = Product.From(product);

            return resp;
        }

        public object Any(Products request)
        {
            var resp = new Dto<List<Product>>();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            resp.Result = productHandler.List(request.Skip, request.Take, request.IncludeDeleted)
                                        .Map(product =>
                                        {
                                            var p = Product.From(product);
                                            p.QuantityOnHand = inventoryHandler.GetQuantityOnHand(p.Id);
                                            return p;
                                        });

            return resp;
        }

        public object Any(ProductTypeahead request)
        {
            var resp = new Dto<List<Product>>();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            List<Models.Product> intermediate;

            if (request.Query.IsNullOrEmpty())
                intermediate = productHandler.List(0, int.MaxValue);
            else
                intermediate = searchHandler.ProductTypeahead(request.Query);

            resp.Result = intermediate.Map(product =>
            {
                var p = Product.From(product);
                p.QuantityOnHand = inventoryHandler.GetQuantityOnHand(p.Id);
                return p;
            });

            return resp;
        }
    }
}
