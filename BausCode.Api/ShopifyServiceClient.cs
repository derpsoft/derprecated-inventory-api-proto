using ServiceStack;

namespace BausCode.Api
{
    public class ShopifyServiceClient : JsonServiceClient
    {
        public ShopifyServiceClient(string baseUri)
            : base(baseUri)
        {
        }
    }
}