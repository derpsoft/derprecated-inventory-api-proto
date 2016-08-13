﻿using BausCode.Api.Handlers;
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

            resp.Product = Models.Dto.Product.From(product);

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

            resp.Products = products.Map(Models.Dto.Product.From);

            return resp;
        }
    }
}