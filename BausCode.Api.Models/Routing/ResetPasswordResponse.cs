namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    public class ResetPasswordResponse
    {
        public string Message { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
        public bool Success { get; set; }
    }
}
