using System.Collections.Generic;
using System.Linq;
using BausCode.Api.Handlers;
using BausCode.Api.Models;
using BausCode.Api.Models.Routing;
using ServiceStack;

namespace BausCode.Api.Services
{
    public class ProductService : BaseService
    {
        public GetProductResponse Any(GetProduct request)
        {
            var resp = new GetProductResponse();
            var handler = new ProductHandler(Db, CurrentUser);

            var product = handler.GetProduct(request.Id);

            resp.Product["id"] = request.Id;

            var fields = request.Fields ?? Product.FIELDS.Keys.ToList();
            foreach (var k in fields)
            {
                resp.Product[k] = product.Get(k);
            }

            return resp;
        }

        public GetProductQuantityOnHandResponse Any(GetProductQuantityOnHand request)
        {
            var resp = new GetProductQuantityOnHandResponse();
            var handler = new ProductHandler(Db, CurrentUser);

            resp.Quantity = handler.GetQuantityOnHand(request.Id);

            return resp;
        }

        public UpdateProductResponse Any(UpdateProduct request)
        {
            var resp = new UpdateProductResponse();
            var handler = new ProductHandler(Db, CurrentUser);
            handler.Update(request.Id, request.Product);
            return resp;
        }

        public object Any(GetProducts request)
        {
            var resp = new GetProductsResponse();
            var handler = new ProductHandler(Db, CurrentUser);
            var products = handler.GetProducts(request.Skip, request.Take);

            resp.Products = products.Map(p =>
            {
                var result = new Dictionary<string, object>();
                var fields = Product.FIELDS.Keys.ToList();
                result["id"] = p.Id;
                result["createdAt"] = p.CreateDate;
                result["updatedAt"] = p.ModifyDate;
                result["version"] = p.RowVersion;

                if (request.MetaOnly.GetValueOrDefault(false))
                {
                    result["fields"] = fields;
                }
                else
                {
                    foreach (var k in fields)
                    {
                        result[k] = p.Get(k);
                    }
                }

                return result;
            });

            return resp;
        }
    }
}