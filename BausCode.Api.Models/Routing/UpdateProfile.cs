namespace BausCode.Api.Models.Routing
{
    using Attributes;
    using ServiceStack;

    [EnsureHttps(SkipIfDebugMode = true, SkipIfXForwardedFor = true)]
    [Authenticate]
    [Route("/api/v1/me", "PUT, POST, PATCH")]
    public class UpdateProfile : IReturn<ProfileResponse>
    {
        [Whitelist]
        public string DisplayName { get; set; }

        [Whitelist]
        public string PhoneNumber { get; set; }

        [Whitelist]
        public string UserName { get; set; }
    }
}
