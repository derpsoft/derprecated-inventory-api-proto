namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Handlers;
    using Models.Dto;
    using Models.Shopify;
    using ServiceStack;
    using ServiceStack.Logging;
    using Image = Models.Dto.Image;
    using Product = Models.Dto.Product;

    public class ProductService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (ProductService));
        public ShopifyServiceClient ShopifyServiceClient { get; set; }

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
            var shopifyHandler = new ShopifyHandler(ShopifyServiceClient);

            var product = productHandler.Save(new Models.Product().PopulateWith(request));
            var shopifyProduct = Models.Shopify.Product.From(product);

            if (shopifyProduct.Id.HasValue)
            {
                shopifyHandler.Update(shopifyProduct);
            }
            else
            {
                shopifyProduct.Variants = new List<Variant>
                {
                    new Variant
                    {
                        Option1 = product.Color,
                        Barcode = product.Barcode,
                        Sku = product.Sku,
                        Price = product.Price.ToString(CultureInfo.InvariantCulture),
                        Weight = product.Weight,
                        WeightUnit = product.WeightUnit
                    }
                };
                shopifyProduct = shopifyHandler.Create(shopifyProduct);
                // ReSharper disable once PossibleInvalidOperationException
                productHandler.SetShopifyId(product.Id, shopifyProduct.Id.Value);
                product.ShopifyId = shopifyProduct.Id.Value;
            }

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

        public object Get(ProductImage request)
        {
            var resp = new Dto<Image>();

            return resp;
        }

        public object Delete(ProductImage request)
        {
            throw new NotImplementedException();
            var resp = new Dto<Image>();

            return resp;
        }

        public object Any(ProductImage request)
        {
            var resp = new Dto<Image>();
            if (Request.Files.Length > 0)
            {
                var productHandler = new ProductHandler(Db, CurrentSession);
                var product = productHandler.Get(request.ProductId);

                if (null != product)
                {
                    var imageHandler = ResolveService<ImageHandler>();
                    var uri = imageHandler.SaveImage(Request.Files.First(), "products");

                    resp.Result = Image.From(productHandler.SaveImage(product.Id, new Models.ProductImage
                    {
                        ProductId = product.Id,
                        SourceUrl = uri.ToString()
                    }));
                }
            }
            return resp;
        }
    }
}
