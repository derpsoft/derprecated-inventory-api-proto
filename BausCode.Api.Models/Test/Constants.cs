using System;
using ServiceStack;

namespace BausCode.Api.Models.Test
{
    public static class Constants
    {
        public static readonly UserSession UnitTestUserSession = new UserSession()
        {
            Email = "test@derprecated.com",
            CreatedAt = DateTime.Now.AddDays(-1),
            UserName = "unitTester",
            FullName = "Unit Test Robot",
            Id = SessionExtensions.CreateRandomSessionId(),
            UserAuthId = "1"
        };

        public static class Authors
        {
            public const string James = "James Cunningham <james@derprecated.com>";
            public const string Allen = "Allen Tong <allen@derprecated.com>";
        }
    }
}