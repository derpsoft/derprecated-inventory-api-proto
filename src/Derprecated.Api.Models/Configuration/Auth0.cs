namespace Derprecated.Api.Models.Configuration
{
    public class Auth0
    {
        public string Audience { get; set; }
        public string Authority { get; set; }
        public string Domain { get; set; }
        public string Jwks { get; set; }
        public string ClientId { get; set; }
        public string Issuer { get; set; }
    }
}
