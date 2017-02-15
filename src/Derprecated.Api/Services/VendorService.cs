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

        public VendorHandler Handler { get; set; }

        public object Any(VendorCount request)
        {
            var resp = new Dto<long>();

            resp.Result = Handler.Count();

            return resp;
        }

        public object Get(Vendor request)
        {
            var resp = new Dto<Vendor>();

            resp.Result = Vendor.From(Handler.Get(request.Id));

            return resp;
        }

        public object Delete(Vendor request)
        {
            var resp = new Dto<Vendor>();

            resp.Result = Vendor.From(Handler.Delete(request.Id));

            return resp;
        }

        public object Any(Vendor request)
        {
            var resp = new Dto<Vendor>();
            var vendor = new Models.Vendor().PopulateWith(request);

            resp.Result = Vendor.From(Handler.Save(vendor));

            return resp;
        }

        public object Get(Vendors request)
        {
            var resp = new Dto<List<Vendor>>();

            resp.Result = Handler.List(request.Skip, request.Take).Map(Vendor.From);

            return resp;
        }

        public object Any(VendorTypeahead request)
        {
            var resp = new Dto<List<Vendor>>();

            if (request.Query.IsNullOrEmpty())
                resp.Result = Handler.List(0, int.MaxValue).Map(Vendor.From);
            else
                resp.Result = Handler.Typeahead(request.Query, request.IncludeDeleted).Map(Vendor.From);

            return resp;
        }
    }
}
