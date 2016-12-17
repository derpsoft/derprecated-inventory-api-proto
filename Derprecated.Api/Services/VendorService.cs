namespace Derprecated.Api.Services
{
    using Handlers;
    using Models.Dto;
    using Models.Routing;
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

        public object Any(CreateVendor request)
        {
            var resp = new VendorResponse();
            var handler = new VendorHandler(Db, CurrentSession);
            var newVendor = handler.Save(new Models.Vendor
                                         {
                                             Name = request.Name
                                         });

            resp.Vendor = Vendor.From(newVendor);

            return resp;
        }
    }
}
