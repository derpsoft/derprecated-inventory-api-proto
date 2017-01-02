namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    public class SaleResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public Dto.Sale Sale { get; set; }
    }
}
