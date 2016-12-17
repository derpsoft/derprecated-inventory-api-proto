namespace Derprecated.Api.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Authentication;
    using Models;
    using ServiceStack;

    public class ApiProxy
    {
        public ApiProxy(string baseUri)
        {
            BaseUri = new Uri(baseUri);
        }

        private Uri BaseUri { get; }

        public BearerOAuth2TokenResponse Authenticate(BearerOAuth auth, BearerOAuth2Token request)
        {
            BearerOAuth2TokenResponse response;
            var uri = new Uri(BaseUri, request.ToPostUrl()).ToString();
            var client = new HybridClient<WwwFormUrlEncodedClient, JsonServiceClient>();

            client.RequestFilter = webRequest => auth.SignRequest(webRequest, request);
            response = client.Post<BearerOAuth2TokenResponse>(uri, request);

            return response;
        }

        public Stream StreamStatusesFilter(AuthBase auth, StatusesFilter request)
        {
            var uri = new Uri(BaseUri, request.ToPostUrl()).ToString();
            var client = new WwwFormUrlEncodedClient();

            client.UserAgent = "Derprecated/1.0 (Derprecated.com)";
            client.RequestFilter = webRequest => auth.SignRequest(webRequest, request);

            return client.Post<Stream>(uri, request);
        }

        public SearchTweetsResponse SearchTweets(AuthBase auth, Counter counter, SearchTweets request)
        {
            SearchTweetsResponse response;
            var client = new JsonServiceClient(BaseUri.ToString());

            client.RequestFilter = webRequest => auth.SignRequest(webRequest, request);
            response = ThrottleContext.Execute(counter, () => client.Get(request));

            return response;
        }

        public List<GetTrendsResponse> GetTrends(AuthBase auth, GetTrends request)
        {
            List<GetTrendsResponse> response;

            var client = new JsonServiceClient(BaseUri.ToString());

            client.RequestFilter = webRequest => auth.SignRequest(webRequest, request);
            response = client.Get(request);

            return response;
        }

        public UpdateStatusResponse UpdateStatus(AuthBase auth, UpdateStatus request)
        {
            UpdateStatusResponse response;
            var uri = new Uri(BaseUri, request.ToPostUrl()).ToString();
            var client = new HybridClient<WwwFormUrlEncodedClient, JsonServiceClient>();

            client.RequestFilter = webRequest => auth.SignRequest(webRequest, request);
            response = client.Post<UpdateStatusResponse>(uri, request);

            return response;
        }
    }
}
