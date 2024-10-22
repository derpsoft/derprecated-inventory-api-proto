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
        private const decimal TAX_RATE =  0.09250m;

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

            public override object Get(Models.Dto.Order request)
            {
                var resp = new Dto<Models.Dto.Order>();
                resp.Result = Handler.Get(request.Id, request.IncludeDeleted).ToDto();
                return resp;
            }

            protected override object Update(Models.Dto.Order request)
            {
              var resp = new Dto<Models.Dto.Order>();
              var newRecord = Handler.Save(request.FromDto(), true);
              resp.Result = newRecord.ToDto();
              return resp;
            }

            public object Post(Models.Dto.OrderBillingCaptured request)
            {
                var resp = new Dto<Models.Dto.Order>();
                var order = Handler.Get(request.Id);

                if(null != order && order.Status.EqualsIgnoreCase(OrderStatus.AwaitingPayment)){
                  var priceInCents = (int)Math.Round(order.Price * 100, 2, MidpointRounding.AwayFromZero);
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
                order.ShipByUserAuthId = CurrentSession.UserAuthId.ToString();
                resp.Result = OrderHandler.Ship(order).ToDto();
              }

              return resp;
            }

            protected override object Create(Models.Dto.Order request)
            {
                var resp = new Dto<Models.Dto.Order>();
                var order = request.FromDto();
                var subtotal = order.Offers.Sum(x => x.Price * x.Quantity);

                order.BillByUserAuthId = CurrentSession.UserAuthId.ToString();
                order.OrderNumber = Order.GetNewOrderNumber();
                order.Price = Math.Round(subtotal + (subtotal * TAX_RATE), 2, MidpointRounding.AwayFromZero);
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
                                     .ConvertAll(x => x.ToDto());
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
