namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class ProductResponse
    {
        public Product Product { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
