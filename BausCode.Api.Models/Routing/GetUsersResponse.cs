using System.Collections.Generic;

namespace BausCode.Api.Models.Routing
{
    public class GetUsersResponse
    {
        public List<Dto.User> Users { get; set; }
        public long Count { get; set; }
    }
}