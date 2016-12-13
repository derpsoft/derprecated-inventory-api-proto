namespace BausCode.Api.Models.Routing
{
    using System.Collections.Generic;
    using ServiceStack;

    public class RolesResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<string> Roles { get; set; }
    }
}
