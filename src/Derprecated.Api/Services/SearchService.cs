﻿namespace Derprecated.Api.Services
{
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;

    public class SearchService : BaseService
    {
        public object Any(UserTypeahead request)
        {
            var resp = new UsersResponse();
            var userHandler = new UserHandler(Db, UserAuthRepository, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Users = userHandler.List(0, int.MaxValue).Map(User.From);
            else
                resp.Users = searchHandler.UserTypeahead(request.Query).Map(User.From);

            return resp;
        }

        public object Any(ProductTypeahead request)
        {
            var resp = new ProductsResponse();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Products = productHandler.List(0, int.MaxValue).Map(Product.From);
            else
                resp.Products = searchHandler.ProductTypeahead(request.Query).Map(Product.From);

            return resp;
        }

        public object Any(VendorTypeahead request)
        {
            var resp = new VendorsResponse();
            var vendorHandler = new VendorHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Vendors = vendorHandler.List(0, int.MaxValue).Map(Vendor.From);
            else
                resp.Vendors = searchHandler.VendorTypeahead(request.Query).Map(Vendor.From);

            return resp;
        }

        public object Any(WarehouseTypeahead request)
        {
            var resp = new WarehousesResponse();
            var warehouseHandler = new WarehouseHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Warehouses = warehouseHandler.List(0, int.MaxValue).Map(Warehouse.From);
            else
                resp.Warehouses = searchHandler.WarehouseTypeahead(request.Query).Map(Warehouse.From);

            return resp;
        }
    }
}
