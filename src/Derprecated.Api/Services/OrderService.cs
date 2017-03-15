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
            public OrderCrudService(IHandler<Order> handler, StripeHandler stripeHandler)
                : base(handler)
            {
              StripeHandler = stripeHandler;
            }

            private StripeHandler StripeHandler {get;set;}

            public object Post(Models.Dto.OrderBillingCaptured request)
            {
                var resp = new Dto<Models.Dto.Order>();

                var order = Handler.Get(request.Id);

                if(null != order && order.Status.EqualsIgnoreCase(OrderStatus.AwaitingPayment)){
                  var priceInCents = (int)order.Price * 100;
                  var charge = StripeHandler.CaptureChargeWithToken(priceInCents, request.Token);
                  Log.Info(charge);
                  order.Status = OrderStatus.AwaitingShipment;
                  Handler.Save(order);
                }

                return resp;
            }

            protected override object Create(Models.Dto.Order request)
            {
                var resp = new Dto<Models.Dto.Order>();
                var order = request.FromDto();

                order.OrderNumber = $"DERP-{DateTime.UtcNow.ToUnixTime()}";
                order.Price = order.AcceptedOffers.Sum(x => x.Price);
                order.Status = OrderStatus.AwaitingPayment;
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
