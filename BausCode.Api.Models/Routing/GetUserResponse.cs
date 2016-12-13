namespace BausCode.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class GetUserResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public User User { get; set; }
    }
}
