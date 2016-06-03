using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using BausCode.Api.Jobs.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Text;

namespace BausCode.Api.Jobs.Authentication
{
    public class ThreeLeggedOAuth : AuthBase
    {
        public ThreeLeggedOAuth(string consumerKey, string consumerSecret, string accessToken,
            string accessTokenSecret,
            IDbConnectionFactory dbConnectionFactory)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            DbConnectionFactory = dbConnectionFactory;
        }

        private string AccessToken { get; set; }
        private string AccessTokenSecret { get; set; }
        private IDbConnectionFactory DbConnectionFactory { get; set; }
        private ThreeLeggedOAuthTokenResponse BearerAuthentication { get; set; }
        private string ConsumerKey { get; set; }
        private string ConsumerSecret { get; set; }

        public override bool IsAuthenticated()
        {
            return !GetToken().IsNullOrEmpty();
        }

        public override string GetToken()
        {
            return AccessToken;
        }

        private string GetNonce()
        {
            var rng = new Random();
            var nonceBytes = new byte[32];
            rng.NextBytes(nonceBytes);
            return Regex.Replace(Convert.ToBase64String(nonceBytes), @"[^\w]", "");
        }

        private string GetSignature(string httpMethod, Uri requestUri, object dto)
        {
            var ts = DateTime.UtcNow.ToUnixTime();
            var nonce = GetNonce();

            var baseParams = new List<KeyValuePair<string, string>>
            {
                Pair(@"oauth_consumer_key", ConsumerKey),
                Pair(@"oauth_nonce", nonce),
                Pair(@"oauth_signature_method", @"HMAC-SHA1"),
                Pair(@"oauth_timestamp", ts.ToString()),
                Pair(@"oauth_token", GetToken()),
                Pair(@"oauth_version", "1.0")
            };

            var dtoParams = dto.ToStringDictionary();

            baseParams.Add(Pair(@"oauth_signature",
                CalculateSignature(httpMethod, requestUri, baseParams.Concat(dtoParams).ToArray())));

            return
                baseParams.OrderBy(kvp => kvp.Key.PercentEncode())
                    .Select(kvp => "{0}=\"{1}\"".Fmt(kvp.Key.PercentEncode(), kvp.Value.PercentEncode()))
                    .Join(", ");
        }

        private string CalculateSignature(string httpMethod, Uri requestUri,
            params KeyValuePair<string, string>[] values)
        {
            var paramString = values
                .OrderBy(kvp => kvp.Key.PercentEncode())
                .Select(kvp => "{0}={1}".Fmt(kvp.Key.PercentEncode(), kvp.Value.PercentEncode()))
                .Join("&");
            var signatureBaseString = "{0}&{1}&{2}".Fmt(httpMethod.ToUpper(),
                requestUri.ToString().PercentEncode(), paramString.PercentEncode());
            var signingKey = "{0}&{1}".Fmt(ConsumerSecret.PercentEncode(), AccessTokenSecret.PercentEncode());

            var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(signingKey));
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.ASCII.GetBytes(signatureBaseString)));
        }

        public override void SignRequest(HttpWebRequest request, object dto)
        {
            var oAuthHeader = "OAuth {0}".Fmt(GetSignature(request.Method, request.RequestUri, dto));
            request.Headers.Add("Authorization", oAuthHeader);
        }

        public static KeyValuePair<T1, T2> Pair<T1, T2>(T1 key, T2 value)
        {
            return new KeyValuePair<T1, T2>(key, value);
        }
    }
}