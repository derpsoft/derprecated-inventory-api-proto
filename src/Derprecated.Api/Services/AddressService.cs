namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using Address = Models.Address;

    public static class AddressServices
    {
        private static ILog Log = LogManager.GetLogger(typeof(AddressServices));

        public class AddressService : CrudService<Address, Models.Dto.Address>
        {
            public AddressService(IHandler<Address> handler) : base(handler)
            {
            }

            public object Any(AddressTypeahead request)
            {
                var resp = new Dto<List<Models.Dto.Address>>();

                if (request.Query.IsNullOrEmpty())
                    resp.Result = Handler.List(0, int.MaxValue, request.IncludeDeleted)
                      .ConvertAll(x => x.ConvertTo<Models.Dto.Address>());
                else
                    resp.Result = Handler.Typeahead(request.Query, request.IncludeDeleted)
                      .ConvertAll(x => x.ConvertTo<Models.Dto.Address>());

                return resp;
            }
        }
    }
}
