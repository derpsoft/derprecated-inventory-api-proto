using System;
using System.Data;
using BausCode.Api.Models;
using BausCode.Api.Models.Routing;

namespace BausCode.Api.Handlers
{
    public class InventoryHandler
    {
        public IDbConnection Db { get; set; }

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
        ///     - product/item mapping
        ///     future
        ///     - trace transaction
        /// </remarks>
        public void Receive(CreateInventoryTransaction request)
        {
        }

        /// <summary>
        ///     Move a quantity of inventory from one warehouse to another. Full or partial quantity acceptable.
        ///     Internally this is the same as calling Release(1) followed by Receive(1)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="quantity"></param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     if the quantity being moved is greater than the quantity available in the given Location
        /// </exception>
        /// <remarks>
        ///     create a warehouse transaction
        ///     - with the quantity being moved as a decrement
        ///         - must not exceed the available quantity
        ///     - with the source Location
        ///     create a warehouse transaction
        ///     - with the quantity being moved as an increment
        ///     - with the target Location
        /// </remarks>
        public void Move(Item item, Location from, Location to, decimal quantity)
        {
        }

        /// <summary>
        /// Release a quantity of inventory to sale, shipment, or other 3rd-party transfer
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// </remarks>
        public void Release(CreateInventoryTransaction request)
        {
        }
    }
}