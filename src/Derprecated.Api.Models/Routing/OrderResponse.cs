namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    public class OrderResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public Dto.Order Order { get; set; }
    }
}
