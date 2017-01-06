namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    public class CountResponse
    {
        public long Count { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
