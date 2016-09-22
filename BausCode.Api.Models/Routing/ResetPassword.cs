using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Routing
{
    [EnsureHttps(SkipIfDebugMode = true, SkipIfXForwardedFor = true)]
    [Route("/api/v1/password/reset", "POST")]
    public class ResetPassword : IReturn<ResetPasswordResponse>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordRepeat { get; set; }
    }
}