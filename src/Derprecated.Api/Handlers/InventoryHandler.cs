namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.OrmLite;

    public class InventoryHandler
    {
        public InventoryHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        /// <summary>
        ///     Receive inventory into the system.
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        ///     create an inventory transaction
        ///     - with the quantity being received
        ///     create a unit of measure
        ///     - matching the quantity being received
        ///     create a warehouse transaction
        ///     - map to location within a warehouse
        ///     - matching the quantity being received
        ///     ensure
        ///     - user captured
        ///     - timestamp captured
        ///     - product/product mapping
        ///     future
        ///     - trace transaction
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">if the requested Quantity is negative.</exception>
        public void Receive(CreateInventoryTransaction request)
        {
            Receive(request.ProductId, request.LocationId, request.Quantity);
        }

        public void Receive(int productId, int locationId, decimal quantity)
        {
            productId.ThrowIfLessThan(1);
            locationId.ThrowIfLessThan(1);
            quantity.ThrowIfLessThan(1);

            var transaction = new InventoryTransaction();
            var product = new ProductHandler(Db).Get(productId);
            //var location = new LocationHandler(Db, User).Get(locationId);

            product.ThrowIfNull(nameof(product));
            //location.ThrowIfNull(nameof(location));

            transaction.ProductId = product.Id;
            transaction.Quantity = quantity;
            transaction.TransactionType = InventoryTransactionTypes.In;
            transaction.UserId = User.UserAuthId.ToInt();
            transaction.UnitOfMeasureId = 1;

            Db.Save(transaction);
        }

        /// <summary>
        ///     Move a quantity of inventory from one warehouse to another. Full or partial quantity acceptable.
        ///     Internally this is the same as calling Release(1) followed by Receive(1)
        /// </summary>
        /// <param name="product"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="quantity"></param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     if the quantity being moved is greater than the quantity available in the given Location
        /// </exception>
        /// <remarks>
        ///     create a warehouse transaction
        ///     - with the quantity being moved as a decrement
        ///     - must not exceed the available quantity
        ///     - with the source Location
        ///     create a warehouse transaction
        ///     - with the quantity being moved as an increment
        ///     - with the target Location
        /// </remarks>
        public void Move(Product product, Location from, Location to, decimal quantity)
        {
            var q = Math.Abs(quantity);
            Release(product.Id, from.Id, -q);
            Receive(product.Id, to.Id, q);
        }

        /// <summary>
        ///     Release a quantity of inventory to sale, shipment, or other 3rd-party transfer
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// </remarks>
        public InventoryTransaction Release(CreateInventoryTransaction request)
        {
            return Release(request.ProductId, request.LocationId, request.Quantity);
        }

        public InventoryTransaction Release(int productId, int locationId, decimal quantity)
        {
            productId.ThrowIfLessThan(1);
            locationId.ThrowIfLessThan(1);
            quantity.ThrowIfGreaterThan(0);

            var transaction = new InventoryTransaction();
            var product = new ProductHandler(Db).Get(productId);
            //var location = new LocationHandler(Db, User).Get(locationId);

            product.ThrowIfNull(nameof(product));
            //location.ThrowIfNull(nameof(location));

            transaction.ProductId = product.Id;
            transaction.Quantity = quantity;
            transaction.TransactionType = InventoryTransactionTypes.Out;
            transaction.UserId = User.UserAuthId.ToInt();
            transaction.UnitOfMeasureId = 1;

            Db.Save(transaction);

            return transaction;
        }

        /// <summary>
        ///     Get quantity on hand for a particular Variant.
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

        public List<InventoryTransaction> List(int skip = 0, int take = 25)
        {
            return Db.Select(
                Db.From<InventoryTransaction>()
                  .Skip(skip)
                  .Take(take)
                );
        }

        public long CountInventoryTransactions()
        {
            return Db.Count<InventoryTransaction>();
        }
    }
}
