namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Handlers;
    using Models;
    using Models.Dto;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Logging;
    using ServiceStack.Text;
    using Order = Models.Order;

    public static class OrderServices
    {
        private static ILog Log = LogManager.GetLogger(typeof(OrderServices));

        public class OrderCrudService : CrudService<Order, Models.Dto.Order>
        {
            public OrderCrudService(IHandler<Order> handler, StripeHandler stripeHandler, ApplicationConfiguration config)
                : base(handler)
            {
              StripeHandler = stripeHandler;
              Config = config;
            }

            private OrderHandler OrderHandler => (OrderHandler)Handler;
            private ApplicationConfiguration Config {get;}
            private StripeHandler StripeHandler {get;}

            public object Post(Models.Dto.OrderBillingCaptured request)
            {
                var resp = new Dto<Models.Dto.Order>();
                var order = Handler.Get(request.Id);

                if(null != order && order.Status.EqualsIgnoreCase(OrderStatus.AwaitingPayment)){
                  var priceInCents = (int)order.Price * 100;
                  var prefixedOrderNumber = $"{Config.App.OrderPrefix}-{order.OrderNumber}";
                  var description = $"Custom order {prefixedOrderNumber}";
                  var stripeCharge = StripeHandler.CaptureChargeWithToken(priceInCents, prefixedOrderNumber, request.Token, description);

                  order.StripeCharge = stripeCharge;
                  order.PaymentMethod = stripeCharge.Source.Brand;
                  order.PaymentMethodId = stripeCharge.Source.Last4;
                  order.Status = OrderStatus.AwaitingShipment;
                  order.BillDate = DateTime.UtcNow;

                  Handler.Save(order);
                }

                return resp;
            }

            public object Post(Models.Dto.OrderShipped request)
            {
              var resp = new Dto<Models.Dto.Order>();
              var order = Handler.Get(request.Id);

              if(null != order && order.Status.Equals(OrderStatus.AwaitingShipment))
              {
                order.ShippingUserAuthId = CurrentSession.UserAuthId.ToString();
                resp.Result = OrderHandler.Ship(order).ToDto();
              }

              return resp;
            }

            protected override object Create(Models.Dto.Order request)
            {
                var resp = new Dto<Models.Dto.Order>();
                var order = request.FromDto();

                order.BillingUserAuthId = CurrentSession.UserAuthId.ToString();
                order.OrderNumber = Order.GetNewOrderNumber();
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

            public object Get(Models.Dto.OrderSummary request)
            {
                var resp = new Dto<Models.Dto.OrderSummary>();

                var order = OrderHandler.GetByKey(request.OrderNumber, request.Key);
                resp.Result = order.ToSummary();

                return resp;
            }
        }
    }
}
