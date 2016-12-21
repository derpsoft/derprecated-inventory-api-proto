namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using ServiceStack;

    public class PermissionsResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<string> Permissions { get; set; }
    }
}
