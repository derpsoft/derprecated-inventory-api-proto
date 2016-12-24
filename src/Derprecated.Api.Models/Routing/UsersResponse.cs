namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using Dto;
    using ServiceStack;

    public class UsersResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<User> Users { get; set; }
    }
}
