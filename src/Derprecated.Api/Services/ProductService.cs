namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Handlers;
    using Models;
    using Models.Routing;
    using Models.Shopify;
    using ServiceStack;
    using ServiceStack.Logging;
    using CountProducts = Models.Routing.CountProducts;
    using CountResponse = Models.Routing.CountResponse;
    using GetProduct = Models.Routing.GetProduct;
    using GetProducts = Models.Routing.GetProducts;
    using Product = Models.Dto.Product;
    using ProductResponse = Models.Routing.ProductResponse;

    public class ProductService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (ProductService));
        public ShopifyServiceClient ShopifyServiceClient { get; set; }

        public object Any(CountProducts request)
        {
            var resp = new CountResponse();
            var handler = new ProductHandler(Db, CurrentSession);

            resp.Count = handler.Count();

            return resp;
        }

        public object Any(GetProduct request)
        {
            var resp = new ProductResponse();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);
            var product = productHandler.GetProduct(request.Id);

            resp.Product = Product.From(product);
            resp.Product.QuantityOnHand = inventoryHandler.GetQuantityOnHand(product.Id);

            return resp;
        }


        public object Any(DeleteProduct request)
        {
            throw new NotImplementedException();
            var resp = new DeleteProductResponse();
            var handler = new ProductHandler(Db, CurrentSession);

            return resp;
        }

        public object Any(GetProductQuantityOnHand request)
        {
            var resp = new QuantityOnHandResponse();
            //TODO(jcunningham) move to inventory service
            var handler = new InventoryHandler(Db, CurrentSession);

            resp.Quantity = handler.GetQuantityOnHand(request.Id);

            return resp;
        }

        public object Any(SaveProduct request)
        {
            var resp = new SaveProductResponse();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var shopifyHandler = new ShopifyHandler(ShopifyServiceClient);

            var product = productHandler.Save(request.Product);
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

            resp.Product = Product.From(product);

            return resp;
        }

        private object UpdateProductField<T>(IUpdatableField<T> request)
        {
            var resp = new ProductResponse();

            var handler = new ProductHandler(Db, CurrentSession);
            handler.Update(request.Id, request);

            return resp;
        }

        public object Any(UpdateProductTitle request)
        {
            return UpdateProductField(request);
        }

        public object Any(UpdateProductDescription request)
        {
            return UpdateProductField(request);
        }

        public object Any(GetProducts request)
        {
            var resp = new GetProductsResponse();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var inventoryHandler = new InventoryHandler(Db, CurrentSession);

            var products = productHandler.List(request.Skip, request.Take);
            resp.Products = products.Map(product =>
                                         {
                                             var p = Product.From(product);
                                             p.QuantityOnHand = inventoryHandler.GetQuantityOnHand(p.Id);
                                             return p;
                                         });
            resp.Count = productHandler.Count();

            return resp;
        }
    }
}
