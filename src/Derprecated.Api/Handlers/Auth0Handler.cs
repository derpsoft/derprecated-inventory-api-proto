namespace Derprecated.Api.Handlers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Auth0.AuthenticationApi;
    using Auth0.Core;
    using Auth0.Core.Collections;
    using Auth0.ManagementApi;
    using Auth0.ManagementApi.Models;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Logging;

    public class Auth0Handler
    {
        private static ILog Log = LogManager.GetLogger(typeof(Auth0Handler));

        private string Audience => $"https://{Configuration.Auth0.Domain}/api/v2/";

        public AuthenticationApiClient AuthClient { get; set; }
        public ApplicationConfiguration Configuration { get; set; }
        private static ClientCredentials Credentials { get; set; }

        private bool IsTokenAlmostExpired()
        {
            return (null == Credentials) || Credentials.IsExpired();
        }

        private ManagementApiClient GetManagementApiClient()
        {
            if (IsTokenAlmostExpired())
                RefreshToken();
            return new ManagementApiClient(Credentials.access_token, new Uri(Audience));
        }

        private void RefreshToken()
        {
            using (var client = new JsonServiceClient($"https://{Configuration.Auth0.Domain}"))
            {
                var request = new ClientCredentialsRequest
                {
                    client_id = Configuration.Auth0.ClientId,
                    client_secret = Configuration.Auth0.ClientSecret,
                    audience = Audience
                };
                // ReSharper disable once RedundantTypeArgumentsOfMethod
                var result = client.Post<ClientCredentials>(request);

                Credentials = result;
            }
        }

        public User GetUser(string id)
        {
            var client = GetManagementApiClient();
            var req = client.Users.GetAsync(id);
            req.Wait();
            return req.Result;
        }

        public IPagedList<User> GetAllUsers(int page = 0, int perPage = 50)
        {
            var client = GetManagementApiClient();
            var req = client.Users.GetAllAsync(page, perPage);
            req.Wait();
            return req.Result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [Route("/oauth/token", "POST")]
    public sealed class ClientCredentialsRequest : IReturn<ClientCredentials>
    {
        public string audience { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; } = "client_credentials";
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class ClientCredentials
    {
        public string access_token { get; set; }

        public DateTime CreatedAt { get; } = DateTime.Now;
        public int expires_in { get; set; }
        public string token_type { get; set; }

        public bool IsExpired()
        {
            return DateTime.Now >= CreatedAt.AddSeconds(expires_in - 60);
        }
    }
}
