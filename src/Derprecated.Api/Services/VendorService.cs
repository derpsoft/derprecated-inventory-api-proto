namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Logging;

    public class VendorService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (VendorService));

        public object Any(VendorCount request)
        {
            var resp = new Dto<long>();
            var handler = new VendorHandler(Db, CurrentSession);

            resp.Result = handler.Count();

            return resp;
        }

        public object Get(Vendor request)
        {
            var resp = new Dto<Vendor>();
            var handler = new VendorHandler(Db, CurrentSession);

            resp.Result = Vendor.From(handler.GetVendor(request.Id));

            return resp;
        }

        public object Any(Vendor request)
        {
            var resp = new Dto<Vendor>();
            var handler = new VendorHandler(Db, CurrentSession);
            var vendor = new Models.Vendor().PopulateWith(request);

            resp.Result = Vendor.From(handler.Save(vendor));

            return resp;
        }

        public object Get(Vendors request)
        {
            var resp = new Dto<List<Vendor>>();
            var handler = new VendorHandler(Db, CurrentSession);

            resp.Result = handler.List(request.Skip, request.Take).Map(Vendor.From);

            return resp;
        }
    }
}
