using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    public class ResetPasswordResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}