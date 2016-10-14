using BausCode.Api.Handlers;
using BausCode.Api.Models;
using BausCode.Api.Models.Routing;
using ServiceStack;
using ServiceStack.Logging;
using Product = BausCode.Api.Models.Dto.Product;

namespace BausCode.Api.Services
{
    public class ProductService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (ProductService));

        public object Any(GetProduct request)
        {
            var resp = new GetProductResponse();
            var handler = new ProductHandler(Db, CurrentSession);

            var product = handler.GetProduct(request.Id);

            resp.Product = Product.From(product);

            return resp;
        }

        public object Any(GetProductQuantityOnHand request)
        {
            var resp = new GetProductQuantityOnHandResponse();
            var handler = new ProductHandler(Db, CurrentSession);

            resp.Quantity = handler.GetQuantityOnHand(request.Id);

            return resp;
        }

        public object Any(UpdateProduct request)
        {
            var resp = new UpdateProductResponse();
            var handler = new ProductHandler(Db, CurrentSession);

            resp.Product = Product.From(handler.Update(request.Id, request));

            return resp;
        }

        private object UpdateProductField<T>(IUpdatableField<T> request)
        {
            var resp = new UpdateProductResponse();

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

            resp.Products = products.Map(Product.From);

            return resp;
        }

        public object Any(SearchProducts request)
        {
            var resp = new SearchProductsResponse();
            var handler = new ProductHandler(Db, CurrentSession);
            var products = handler.Search(request.Query, request.Skip, request.Take);

            resp.Products = products.Map(Product.From);

            return resp;
        }
    }
}