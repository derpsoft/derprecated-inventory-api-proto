namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
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
            var resp = new Dto<List<Product>>();
            var productHandler = new ProductHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = productHandler.List(0, int.MaxValue).Map(Product.From);
            else
                resp.Result = searchHandler.ProductTypeahead(request.Query).Map(Product.From);

            return resp;
        }

        public object Any(VendorTypeahead request)
        {
            var resp = new Dto<List<Vendor>>();
            var vendorHandler = new VendorHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = vendorHandler.List(0, int.MaxValue).Map(Vendor.From);
            else
                resp.Result = searchHandler.VendorTypeahead(request.Query).Map(Vendor.From);

            return resp;
        }

        public object Any(LocationTypeahead request)
        {
            var resp = new Dto<List<Location>>();
            var locationHandler = new LocationHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = locationHandler.List(0, int.MaxValue).Map(Location.From);
            else
                resp.Result = searchHandler.LocationTypeahead(request.Query).Map(Location.From);

            return resp;
        }
    }
}
