namespace BausCode.Api.Models.Routing
{
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [EnsureHttps(SkipIfDebugMode = true, SkipIfXForwardedFor = true)]
    [Route("/api/v1/password/forgot", "POST")]
    public class ForgotPassword : IReturn<ForgotPasswordResponse>
    {
        [Required]
        public string Email { get; set; }
    }
}
