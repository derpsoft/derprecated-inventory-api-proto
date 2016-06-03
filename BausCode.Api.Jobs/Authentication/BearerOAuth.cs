using System;
using System.Net;
using System.Text;
using BausCode.Api.Jobs.Models;
using ServiceStack;

namespace BausCode.Api.Jobs.Authentication
{
    public class BearerOAuth : AuthBase
    {
        public BearerOAuth(string consumerKey, string consumerSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        private BearerOAuth2TokenResponse BearerAuthentication { get; set; }
        private string ConsumerKey { get; set; }
        private string ConsumerSecret { get; set; }

        public override bool IsAuthenticated()
        {
            return !RequiresReauthentication();
        }

        public override string GetToken()
        {
            return BearerAuthentication.AccessToken;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        /// <remarks>
        ///     Use IServiceClient.SetCredentials(key, secret); where possible instead of doing this by hand.
        /// </remarks>
        public static string EncodeCredentials(string key, string secret)
        {
            var cleartext = string.Concat(key.UrlEncode(), ':', secret.UrlEncode());
            var base64bytes = Encoding.UTF8.GetBytes(cleartext);
            return Convert.ToBase64String(base64bytes);
        }

        /// <summary>
        ///     Checks if this instance needs to reauthenticate, and if needed, does so.
        /// </summary>
        public void Reauthenticate(ApiProxy apiProxy)
        {
            if (RequiresReauthentication())
            {
                var request = new BearerOAuth2Token {ConsumerKey = ConsumerKey, ConsumerSecret = ConsumerSecret};

                request.RequestToken = EncodeCredentials(request.ConsumerKey, request.ConsumerSecret);
                BearerAuthentication = apiProxy.Authenticate(this, request);
            }
        }

        /// <summary>
        ///     Returns true if the calling instance should reauthenticate.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     Application-authorized bearer tokens don't expire on a timeline.
        /// </remarks>
        private bool RequiresReauthentication()
        {
            return null == BearerAuthentication;
        }

        public override void SignRequest(HttpWebRequest request, object dto)
        {
            request.Headers.Remove("Authorization");

            var token = dto as BearerOAuth2Token;
            if (token != null)
            {
                request.Headers.Add("Authorization", "Basic {0}".Fmt(token.Credentials));
            }
            else
            {
                request.Headers.Add("Authorization", "Bearer {0}".Fmt(GetToken()));
            }
        }
    }
}