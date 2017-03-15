namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using Models;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    public class OrderHandler : CrudHandler<Order>
    {
        public OrderHandler(IDbConnectionFactory db)
            : base(db)
        {
        }

        protected override void BeforeDelete(Order record)
        {
            record.Status = OrderStatus.Cancelled;
        }

        public override List<Order> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Order>()
                .Where(x => x.OrderNumber.Contains(q));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
        }
    }
}
