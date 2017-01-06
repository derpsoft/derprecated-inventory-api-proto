﻿namespace Derprecated.Api.Services
{
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Logging;

    public class VendorService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (VendorService));

        public object Any(CountVendors request)
        {
            var resp = new CountResponse();
            var handler = new VendorHandler(Db, CurrentSession);

            resp.Count = handler.Count();

            return resp;
        }

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
            var vendor = new Models.Vendor().PopulateWith(request);

            resp.Vendor = Vendor.From(handler.Save(vendor));

            return resp;
        }

        public object Any(UpdateVendor request)
        {
            var resp = new VendorResponse();
            var handler = new VendorHandler(Db, CurrentSession);
            var update = new Models.Vendor().PopulateWith(request);

            resp.Vendor = Vendor.From(handler.Save(update));

            return resp;
        }
    }
}
