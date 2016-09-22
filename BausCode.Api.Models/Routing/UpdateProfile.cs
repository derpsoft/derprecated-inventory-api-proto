using BausCode.Api.Models.Attributes;
using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [EnsureHttps(SkipIfDebugMode = true, SkipIfXForwardedFor = true)]
    [Authenticate]
    [Route("/api/v1/me", "PUT, POST, PATCH")]
    public class UpdateProfile : IReturn<ProfileResponse>
    {
        [Whitelist]
        public string DisplayName { get; set; }

        [Whitelist]
        public string UserName { get; set; }

        [Whitelist]
        public string PhoneNumber { get; set; }
    }
}