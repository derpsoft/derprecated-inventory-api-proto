using BausCode.Api.Handlers;
using BausCode.Api.Models.Dto;
using BausCode.Api.Models.Routing;
using ServiceStack;

namespace BausCode.Api.Services
{
    public class ProductService : BaseService
    {
        public object Any(GetProduct request)
        {
            var resp = new GetProductResponse();
            var handler = new ProductHandler(Db, CurrentUser);

            var product = handler.GetProduct(request.Id);

            resp.Product = Product.From(product);

            return resp;
        }

        public object Any(GetProductQuantityOnHand request)
        {
            var resp = new GetProductQuantityOnHandResponse();
            var handler = new ProductHandler(Db, CurrentUser);

            resp.Quantity = handler.GetQuantityOnHand(request.Id);

            return resp;
        }

        public object Any(UpdateProduct request)
        {
            var resp = new UpdateProductResponse();
            var handler = new ProductHandler(Db, CurrentUser);

            resp.Product = Product.From(handler.Update(request.Id, request));

            return resp;
        }

        public object Any(GetProducts request)
        {
            var resp = new GetProductsResponse();
            var handler = new ProductHandler(Db, CurrentUser);
            var products = handler.GetProducts(request.Skip, request.Take);

            resp.Products = products.Map(Product.From);

            return resp;
        }
    }
}