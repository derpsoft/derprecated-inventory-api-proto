namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    public class ProductResponse
    {
        public Product Product { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
