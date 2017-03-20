namespace Derprecated.Api.Handlers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Models;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    public class OrderHandler : CrudHandler<Order>
    {
        public OrderHandler(IDbConnectionFactory db, ApplicationConfiguration config)
            : base(db)
        {
          Configuration = config;
        }

        protected ApplicationConfiguration Configuration { get; }

        protected override void BeforeDelete(Order record)
        {
            record.Status = OrderStatus.Cancelled;
        }

        protected override void BeforeCreate(Order record)
        {
            record.OrderKey = record.GetKey(Configuration.App.OrderSalt);
        }

        public override Order Save(Order record, bool includeReferences = true)
        {
          return base.Save(record, includeReferences);
        }

        public override List<Order> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Order>()
                .Where(x => x.OrderNumber == (ulong)q.ToInt64(0));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
        }

        public Order Ship(Order order)
        {
          order.ThrowIfNull();

          if(!order.Status.Equals(OrderStatus.AwaitingShipment))
          {
            throw new Exception("Order is not ready for shipment");
          }

          order.Status = OrderStatus.Shipped;
          order.ShipDate = DateTime.UtcNow;
          Save(order);

          // TODO(jcunningham)
          // Send email?

          return order;
        }

        public Order GetByNumber(ulong orderNumber, bool includeDeleted = false)
        {
          orderNumber.ThrowIfLessThan(1);

          var query = AddJoinTables(Db.From<Order>())
              .Where(x => x.OrderNumber == orderNumber);

          if (!includeDeleted)
              query = query.Where(x => !x.IsDeleted);

          return Db.LoadSelect(query)
            .First();
        }

        public string GetKey(ulong orderNumber)
        {
          return new Order(){OrderNumber = orderNumber}.GetKey(Configuration.App.OrderSalt);
        }

        public Order GetByKey(ulong orderNumber, string orderKey, bool includeDeleted = false)
        {
          orderNumber.ThrowIfLessThan(1);
          orderKey.ThrowIfNullOrEmpty();

          var order = GetByNumber(orderNumber, includeDeleted);
          if (!order.OrderKey.Equals(orderKey)) {
            throw new ArgumentException("Invalid id/key combination.");
          }
          return order;
        }
    }
}
