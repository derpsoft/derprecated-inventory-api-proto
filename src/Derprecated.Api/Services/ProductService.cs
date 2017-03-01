namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using Image = Models.Dto.Image;
    using Product = Models.Dto.Product;

    public class ProductService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(ProductService));

        public ProductService(IHandler<ProductImage> productImageHandler)
        {
            ProductImageHandler = (ProductImageHandler) productImageHandler;
        }

        protected ProductImageHandler ProductImageHandler { get; }

        public object Any(ProductCount request)
        {
            var resp = new Dto<long>();
            var handler = new ProductHandler(Db);

            resp.Result = handler.Count(request.IncludeDeleted);

            return resp;
        }

        public object Get(Product request)
        {
            var resp = new Dto<Product>();
            var productHandler = new ProductHandler(Db);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            var product = productHandler.Get(request.Id, request.IncludeDeleted).ConvertTo<Product>();

            resp.Result = product;
            resp.Result.QuantityOnHand = inventoryHandler.GetQuantityOnHand(product.Id);
            resp.Result.Images =
                ProductImageHandler.GetImages(request.Id, request.IncludeDeleted).ConvertAll(x => x.ConvertTo<Image>());

            return resp;
        }

        public object Get(ProductBySku request)
        {
            var resp = new Dto<Product>();
            var productHandler = new ProductHandler(Db);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            var product = productHandler.Get(request.Sku, request.IncludeDeleted).ConvertTo<Product>();

            resp.Result = product;
            resp.Result.QuantityOnHand = inventoryHandler.GetQuantityOnHand(product.Id);

            return resp;
        }

        public object Any(Product request)
        {
            var resp = new Dto<Product>();
            var productHandler = new ProductHandler(Db);

            var product = productHandler.Save(new Models.Product().PopulateWith(request));

            resp.Result = product.ConvertTo<Product>();

            return resp;
        }

        public object Any(Products request)
        {
            var resp = new Dto<List<Product>>();
            var productHandler = new ProductHandler(Db);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            resp.Result = productHandler.List(request.Skip, request.Take, request.IncludeDeleted)
                                        .Map(product =>
                                        {
                                            var p = product.ConvertTo<Product>();
                                            p.QuantityOnHand = inventoryHandler.GetQuantityOnHand(p.Id);
                                            return p;
                                        });

            return resp;
        }

        public object Any(ProductTypeahead request)
        {
            var resp = new Dto<List<Product>>();
            var productHandler = new ProductHandler(Db);
            var searchHandler = new SearchHandler(Db, CurrentSession);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            List<Models.Product> intermediate;

            if (request.Query.IsNullOrEmpty())
                intermediate = productHandler.List(0, int.MaxValue);
            else
                intermediate = productHandler.Typeahead(request.Query, request.IncludeDeleted);

            resp.Result = intermediate.Map(product =>
            {
                var p = product.ConvertTo<Product>();
                p.QuantityOnHand = inventoryHandler.GetQuantityOnHand(p.Id);
                return p;
            });

            return resp;
        }
    }

    public class ProductImportService : CreateService<Models.Product, ProductImport>
    {
        public ProductImportService(IHandler<Models.Product> handler) : base(handler)
        {
        }

        private new ProductHandler Handler => base.Handler as ProductHandler;

        protected override object Create(ProductImport request)
        {
            var resp = new Dto<List<Product>>();
            var newRecords = Handler.SaveMany(request.Products.ConvertAll(x => x.ConvertTo<Models.Product>()));
            resp.Result = newRecords.ConvertAll(x => x.ConvertTo<Product>());
            return resp;
        }
    }
}
