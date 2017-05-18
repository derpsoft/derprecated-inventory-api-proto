namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Logging;
    using ServiceStack.OrmLite;

    public class InventoryHandler : IReadHandler<InventoryTransaction>
    {
        protected static ILog Log = LogManager.GetLogger(typeof (InventoryHandler));

        public InventoryHandler(IDbConnection db)
        {
            Db = db;
        }

        private IDbConnection Db { get; }

        public InventoryTransaction Get(int id, bool includeDeleted = false)
        {
          return Db.LoadSelect(QueryForGet(id))
                   .Single();
        }

        public virtual SqlExpression<InventoryTransaction> AddJoinTables(SqlExpression<InventoryTransaction> source)
        {
            return source;
        }

        public virtual SqlExpression<InventoryTransaction> QueryForGet(int id)
        {
            id.ThrowIfLessThan(1);

            var query = AddJoinTables(Db.From<InventoryTransaction>())
                .Where(x => x.Id == id);

            return query;
        }

        public long Count(bool includeDeleted = false)
        {
          return Db.Count<InventoryTransaction>();
        }

        public List<InventoryTransaction> Typeahead(string query, bool includeDeleted = false)
        {
          throw new NotImplementedException();
        }

        public InventoryTransaction Save(InventoryTransaction request)
        {
            request.ThrowIfNull();
            request.Id.ThrowIfGreaterThan(0);
            request.ProductId.ThrowIfLessThan(1);
            request.UserAuthId.ThrowIfNullOrEmpty();

            request.TransactionType = request.Quantity > 0
              ? InventoryTransactionTypes.In : InventoryTransactionTypes.Out;

            Db.Save(request);

            return request;
        }

        /// <summary>
        ///     Get quantity on hand for a particular product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        public decimal GetQuantityOnHand(int productId)
        {
            productId.ThrowIfLessThan(1);

            return Math.Max(0, Db.Scalar<decimal>(
                Db.From<InventoryTransaction>()
                  .Where(it => it.ProductId == productId)
                  .Select(it => Sql.Sum(it.Quantity))
                ));
        }

        public List<InventoryTransaction> List(int skip = 0, int take = 25, bool includeDeleted = false)
        {
            var q = AddJoinTables(Db.From<InventoryTransaction>())
              .OrderByDescending(x => x.CreateDate)
              .Skip(skip)
              .Take(take);

            return Db.LoadSelect<InventoryTransaction>(q);
        }
    }
}
