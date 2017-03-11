namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using Order = Models.Order;

    public static class OrderServices
    {
        private static ILog Log = LogManager.GetLogger(typeof(OrderServices));

        public class OrderCrudService : CrudService<Order, Models.Dto.Order>
        {
            public OrderCrudService(IHandler<Order> handler)
                : base(handler)
            {
            }

            public object Get(Models.Dto.Orders request)
            {
                var resp = new Dto<List<Models.Dto.Order>>();
                resp.Result = Handler.List(request.Skip, request.Take)
                                     .ConvertAll(x => x.ConvertTo<Models.Dto.Order>());
                return resp;
            }
        }
    }
}
