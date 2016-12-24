namespace Derprecated.Api.Services
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
            var handler = new SearchHandler(Db, CurrentSession);

            resp.Users = handler.UserTypeahead(request.Query).Map(User.From);

            return resp;
        }

        public object Any(ProductTypeahead request)
        {
            var resp = new ProductsResponse();
            var handler = new SearchHandler(Db, CurrentSession);

            resp.Products = handler.ProductTypeahead(request.Query).Map(Product.From);

            return resp;
        }

        public object Any(VendorTypeahead request)
        {
            var resp = new VendorsResponse();
            var handler = new SearchHandler(Db, CurrentSession);

            resp.Vendors = handler.VendorTypeahead(request.Query).Map(Vendor.From);

            return resp;
        }

        public object Any(WarehouseTypeahead request)
        {
            var resp = new WarehousesResponse();
            var handler = new SearchHandler(Db, CurrentSession);

            resp.Warehouses = handler.WarehouseTypeahead(request.Query).Map(Warehouse.From);
            return resp;
        }
    }
}
