namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    public class ForgotPasswordResponse
    {
        public string Message { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
