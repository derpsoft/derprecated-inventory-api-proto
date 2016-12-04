namespace BausCode.Api.Jobs.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class BearerOAuth2TokenResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
    }
}
