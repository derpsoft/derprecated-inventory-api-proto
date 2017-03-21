namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Handlers;
    using Models;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Logging;

    public class InventoryService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (InventoryService));

        public InventoryHandler Handler {get;set;}

        public object Get(InventoryTransactions request)
        {
            var resp = new Dto<List<Models.Dto.InventoryTransaction>>();

            resp.Result = Handler.List(request.Skip, request.Take).ConvertAll(x => x.ToDto());

            return resp;
        }

        public object Any(InventoryTransactionCount request)
        {
            var resp = new Dto<long>();

            resp.Result = Handler.Count();

            return resp;
        }

        public object Post(Models.Dto.InventoryTransaction request)
        {
          var resp = new Dto<Models.Dto.InventoryTransaction>();
          var transaction = request.FromDto();

          transaction.UserAuthId = CurrentSession.UserAuthId.ToString();
          resp.Result = Handler.Save(transaction).ToDto();

          return resp;
        }
    }
}
