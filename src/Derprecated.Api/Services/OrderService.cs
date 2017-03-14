namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Handlers;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using ServiceStack.Text;
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

            protected override object Create(Models.Dto.Order request)
            {
                var resp = new Dto<Models.Dto.Order>();
                var order = request.FromDto();

                order.OrderNumber = $"DERP-{DateTime.UtcNow.ToUnixTime()}";
                order.Price = order.AcceptedOffers.Sum(x => x.Price);
                order.OrderStatus = OrderStatus.AwaitingPayment;
                order.PriceCurrency = Currency.USD;

                var newRecord = Handler.Save(order, true);
                resp.Result = newRecord.ConvertTo<Models.Dto.Order>();
                return resp;
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
