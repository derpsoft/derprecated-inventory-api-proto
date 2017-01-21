namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class SaleResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public Sale Sale { get; set; }
    }
}
