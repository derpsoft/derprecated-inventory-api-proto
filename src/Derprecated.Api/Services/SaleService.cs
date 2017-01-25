namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models.Dto;
    using ServiceStack;

    public class SaleService : BaseService
    {
        public object Get(Sale request)
        {
            var resp = new Dto<Sale>();
            var handler = new SaleHandler(Db, CurrentSession);

            resp.Result = Sale.From(handler.Get(request.Id));

            return resp;
        }

        public object Any(Sale request)
        {
            var resp = new Dto<Sale>();
            var handler = new SaleHandler(Db, CurrentSession);

            resp.Result = Sale.From(handler.Create(new Models.Sale().PopulateWith(request)));

            return resp;
        }

        public object Any(Sales request)
        {
            var resp = new Dto<List<Sale>>();
            var handler = new SaleHandler(Db, CurrentSession);

            resp.Result = handler.List(request.Skip, request.Take).Map(Sale.From);

            return resp;
        }

        public object Any(SaleCount request)
        {
            var resp = new Dto<long>();
            var handler = new SaleHandler(Db, CurrentSession);

            resp.Result = handler.Count();

            return resp;
        }

        public object Any(SaleTypeahead request)
        {
            var resp = new Dto<List<Sale>>();
            var saleHandler = new SaleHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = saleHandler.List(0, int.MaxValue).Map(Sale.From);
            else
                resp.Result = searchHandler.SaleTypeahead(request.Query).Map(Sale.From);

            return resp;
        }
    }
}
