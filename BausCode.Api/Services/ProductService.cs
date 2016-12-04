namespace BausCode.Api.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using Handlers;
    using Models;
    using Models.Routing;
    using Models.Shopify;
    using ServiceStack.Logging;
    using GetProduct = Models.Routing.GetProduct;
    using GetProducts = Models.Routing.GetProducts;
    using ProductResponse = Models.Routing.ProductResponse;

    public class ProductService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (ProductService));
        public ShopifyServiceClient ShopifyServiceClient { get; set; }

        public object Any(GetProduct request)
        {
            var resp = new ProductResponse();
            var handler = new ProductHandler(Db, CurrentSession);

            var product = handler.GetProduct(request.Id);

            resp.Product = product;

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

            resp.Product = product;

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
            var handler = new ProductHandler(Db, CurrentSession);
            var products = handler.GetProducts(request.Skip, request.Take);

            resp.Products = products;
            resp.Count = handler.Count();

            return resp;
        }
    }
}
