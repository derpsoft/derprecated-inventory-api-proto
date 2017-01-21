namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class OrderResponse
    {
        public Order Order { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
