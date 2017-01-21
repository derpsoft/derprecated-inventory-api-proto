namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using Dto;

    public class GetUsersResponse
    {
        public long Count { get; set; }
        public List<User> Users { get; set; }
    }
}
