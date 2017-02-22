namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using Vendor = Models.Vendor;

    public static class VendorServices
    {
        private static ILog Log = LogManager.GetLogger(typeof(LocationServices));

        public class VendorService : CrudService<Vendor, Models.Dto.Vendor>
        {
            public VendorService(IHandler<Vendor> handler)
                : base(handler)
            {
            }

            public object Any(VendorCount request)
            {
                var resp = new Dto<long>();

                resp.Result = Handler.Count();

                return resp;
            }

            public object Get(Vendors request)
            {
                var resp = new Dto<List<Models.Dto.Vendor>>();

                resp.Result = Handler.List(request.Skip, request.Take).Map(Models.Dto.Vendor.From);

                return resp;
            }

            public object Any(VendorTypeahead request)
            {
                var resp = new Dto<List<Models.Dto.Vendor>>();

                if (request.Query.IsNullOrEmpty())
                    resp.Result = Handler.List(0, int.MaxValue).Map(Models.Dto.Vendor.From);
                else
                    resp.Result = Handler.Typeahead(request.Query, request.IncludeDeleted).Map(Models.Dto.Vendor.From);

                return resp;
            }
        }
    }
}
