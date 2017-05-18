namespace Derprecated.Api.Models
{
    public static class OrderStatus
    {
        public const string Pending = "pending";
        public const string AwaitingPayment = "awaitingPayment";
        public const string AwaitingFulfillment = "awaitingFulfillment";
        public const string AwaitingShipment = "awaitingShipment";
        public const string PartiallyShipped = "partiallyShipped";
        public const string Shipped = "shipped";
        public const string Cancelled = "cancelled";
        public const string Declined = "declined";
        public const string Refunded = "refunded";
    }
}
