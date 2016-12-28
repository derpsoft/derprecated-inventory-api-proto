namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    public class CountResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public long Count { get; set; }
    }
}
