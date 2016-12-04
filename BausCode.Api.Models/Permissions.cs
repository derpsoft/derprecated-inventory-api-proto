namespace BausCode.Api.Models
{
    public static class Permissions
    {
        /// <summary>
        ///     All permissions.
        /// </summary>
        public const string CanDoEverything = "CanDoEverything";

        /// <summary>
        ///     Read a product.
        /// </summary>
        public const string CanReadProducts = "CanReadProducts";

        /// <summary>
        ///     Create a new product.
        /// </summary>
        public const string CanCreateProducts = "CanCreateProducts";

        /// <summary>
        ///     Update a product.
        /// </summary>
        public const string CanUpdateProducts = "CanUpdateProducts";

        /// <summary>
        ///     Save a product. Equivalent to CanCreateProducts plus CanUpdateProducts.
        /// </summary>
        public const string CanSaveProducts = "CanSaveProducts";

        /// <summary>
        ///     Delete a product.
        /// </summary>
        public const string CanDeleteProducts = "CanDeleteProducts";

        /// <summary>
        ///     Increment inventory.
        /// </summary>
        public const string CanReceiveInventory = "CanReceiveInventory";

        /// <summary>
        ///     Decrement inventory.
        /// </summary>
        public const string CanReleaseInventory = "CanReleaseInventory";

        /// <summary>
        ///     Save inventory. Equivalent to CanReceiveInventory plus CanReleaseInventory
        /// </summary>
        public const string CanSaveInventory = "CanSaveInventory";
    }
}
