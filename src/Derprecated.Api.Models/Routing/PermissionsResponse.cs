namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using ServiceStack;

    public class PermissionsResponse
    {
        public List<string> Permissions { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
