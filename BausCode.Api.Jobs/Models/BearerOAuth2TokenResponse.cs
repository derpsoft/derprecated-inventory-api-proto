using System.Runtime.Serialization;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class BearerOAuth2TokenResponse
    {
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}