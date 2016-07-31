using System.Runtime.Serialization;
using BausCode.Api.Jobs.Authentication;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Jobs.Models
{
    [Route("/oauth2/token", "POST")]
    [DataContract]
    public class BearerOAuth2Token : IReturn<BearerOAuth2TokenResponse>, IHasCredentials
    {
        [DataMember(Name = "grant_type")]
        public string GrantType
        {
            get { return ApiConstants.GRANT_TYPE_CLIENT_CREDENTIALS; }
        }

        /// <summary>
        ///     The URL encoded Twitter API consumer key.
        /// </summary>
        [IgnoreDataMember]
        [Ignore]
        public string ConsumerKey { get; set; }

        /// <summary>
        ///     The URL encoded Twitter API consumer secret.
        /// </summary>
        [IgnoreDataMember]
        [Ignore]
        public string ConsumerSecret { get; set; }

        /// <summary>
        ///     The fully encoded request token. Optional; used when present, else calculated from <see cref="ConsumerKey" /> and
        ///     <see cref="ConsumerSecret" />
        /// </summary>
        [IgnoreDataMember]
        [Ignore]
        public string RequestToken { get; set; }

        public string Credentials
        {
            get { return RequestToken; }
        }
    }
}