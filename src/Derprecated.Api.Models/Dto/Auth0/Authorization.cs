namespace Derprecated.Api.Models.Dto.Auth0
{
    using System.Collections.Generic;

    public class Authorization
    {
        public Authorization()
        {
            Permissions = new List<string>();
            Roles = new List<string>();
        }

        public List<string> Permissions { get; set; }
        public List<string> Roles { get; set; }
    }
}
