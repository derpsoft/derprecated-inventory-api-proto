namespace BausCode.Api.Jobs.Models
{
    public class ThreeLeggedOAuthToken
    {
        public string OAuthConsumerKey { get; set; }
        public string OAuthNonce { get; set; }
        public string OAuthSignatureMethod { get; set; }
        public string OAuthTimestamp { get; set; }
        public string OAuthVersion { get; set; }
        public string Token { get; set; }
    }
}
