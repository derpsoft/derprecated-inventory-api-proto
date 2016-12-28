namespace Derprecated.Api.Services
{
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Logging;

    public class VendorService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (VendorService));

        public object Any(GetVendor request)
        {
            var resp = new VendorResponse();
            var handler = new VendorHandler(Db, CurrentSession);

            resp.Vendor = Vendor.From(handler.GetVendor(request.Id));

            return resp;
        }

        public object Any(GetVendors request)
        {
            var resp = new VendorsResponse();
            var handler = new VendorHandler(Db, CurrentSession);

            resp.Vendors = handler.List(request.Skip, request.Take).Map(Vendor.From);

            return resp;
        }

        public object Any(CreateVendor request)
        {
            var resp = new VendorResponse();
            var handler = new VendorHandler(Db, CurrentSession);
            var newVendor = handler.Save(new Models.Vendor
                                         {
                                             Name = request.Vendor.Name
                                         });

            resp.Vendor = Vendor.From(newVendor);

            return resp;
        }

        public object Any(UpdateVendor request)
        {
            var resp = new VendorResponse();
            var handler = new VendorHandler(Db, CurrentSession);
            var update = handler.Save(new Models.Vendor
                                      {
                                          Id = request.Id
                                      }.PopulateWith(request.Vendor));

            resp.Vendor = Vendor.From(update);


            return resp;
        }
    }
}
