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

            resp.Result = Vendor.From(handler.Get(request.Id));

            return resp;
        }

        public object Delete(Vendor request)
        {
            var resp = new Dto<Vendor>();
            var handler = new VendorHandler(Db, CurrentSession);

            resp.Result = Vendor.From(handler.Delete(request.Id));

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

        public object Any(VendorTypeahead request)
        {
            var resp = new Dto<List<Vendor>>();
            var vendorHandler = new VendorHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = vendorHandler.List(0, int.MaxValue).Map(Vendor.From);
            else
                resp.Result = vendorHandler.Typeahead(request.Query, request.IncludeDeleted).Map(Vendor.From);

            return resp;
        }
    }
}
