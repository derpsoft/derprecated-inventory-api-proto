namespace BausCode.Api.Models
{
    public static class Permissions
    {
        /// <summary>
        ///     All permissions.
        /// </summary>
        public const string CanDoEverything = "everything";

        /// <summary>
        ///     All Product permissions.
        /// </summary>
        public const string CanManageProducts = "manageProducts";

        /// <summary>
        ///     Read a product.
        /// </summary>
        public const string CanReadProducts = "readProducts";

        /// <summary>
        ///     Update a product.
        /// </summary>
        public const string CanUpsertProducts = "upsertProducts";

        /// <summary>
        ///     Delete a product.
        /// </summary>
        public const string CanDeleteProducts = "deleteProducts";

        /// <summary>
        ///     Increment inventory.
        /// </summary>
        public const string CanReceiveInventory = "receiveInventory";

        /// <summary>
        ///     Decrement inventory.
        /// </summary>
        public const string CanReleaseInventory = "releaseInventory";

        /// <summary>
        ///     Manage inventory. Equivalent to CanReceiveInventory plus CanReleaseInventory
        /// </summary>
        public const string CanManageInventory = "manageInventory";

        /// <summary>
        ///     All Vendor permissions.
        /// </summary>
        public const string CanManageVendors = "manageVendors";

        /// <summary>
        ///     Read access to vendors.
        /// </summary>
        public const string CanReadVendors = "readVendors";

        /// <summary>
        ///     Write access to vendors.
        /// </summary>
        public const string CanUpsertVendors = "upsertVendors";

        /// <summary>
        ///     Delete vendors.
        /// </summary>
        public const string CanDeleteVendors = "deleteVendors";
    }
}
