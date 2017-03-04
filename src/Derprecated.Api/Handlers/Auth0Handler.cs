namespace Derprecated.Api.Handlers
{
    using System;
    using Auth0.ManagementApi;
    using Models;
    using ServiceStack.Logging;

    public class Auth0Handler
    {
        private static ILog Log = LogManager.GetLogger(typeof(Auth0Handler));

        public static Auth0Handler Instance { get; private set; }
        public static ApplicationConfiguration  Configuration { get; set; }
        private static AccessKey { get; set; }

        public void RefreshToken()
        {
            var client = new RestClient("https://derprecated.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter(
                "application/json",
                new {
                    grant_type = "client_credentials",
                    client_id = Configuration.Auth0.ClientId,
                    client_secret = Configuration.Auth0.ClientSecret,
                    audience = $"{Configuration.Auth0.Authority}/api/v2/",
                }.ToJson(),
                ParameterType.RequestBody
            );
            IRestResponse response = client.Execute(request);
            
        }
    }
}
