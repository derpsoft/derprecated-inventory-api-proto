namespace BausCode.Api
{
    using ServiceStack;

    public class ShopifyServiceClient : JsonServiceClient
    {
        public ShopifyServiceClient(string baseUri)
            : base(baseUri)
        {
        }
    }
}
