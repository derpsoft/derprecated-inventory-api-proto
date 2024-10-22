﻿namespace Derprecated.Api.Models.Test
{
    using System;
    using ServiceStack;
    using ServiceStack.Auth;

    public static class Constants
    {
        public static readonly UserSession TestUserSession = new UserSession
        {
            Email = "test@derprecated.com",
            CreatedAt = DateTime.Now.AddDays(-1),
            UserName = "unitTester",
            FullName = "Unit Test Robot",
            Id = SessionExtensions.CreateRandomSessionId(),
            UserAuthId = "1"
        };

        public static readonly Authenticate TestAdminAuthenticate = new Authenticate
        {
            provider =
                AuthenticateService.CredentialsProvider,
            UserName = "admin@derprecated.com",
            Password = "123456"
        };

        public static readonly Authenticate TestAuthenticate = new Authenticate
        {
            provider = AuthenticateService.CredentialsProvider,
            UserName = "test@derprecated.com",
            Password = "123456"
        };

        public static class Categories
        {
            public const string Unit = "Unit";
            public const string Integration = "Integration";
        }

        public static class Shopify
        {
            public const long IntegrationTestProductId = 8954660422L;
            public const long IntegrationTestImageId = 20466640070L;
            public const long IntegrationTestVariantId = 32605101894L;
        }

        public static class Authors
        {
            public const string James = "James Cunningham <james@derprecated.com>";
            public const string Allen = "Allen Tong <allen@derprecated.com>";
        }

        public static class Vendors
        {
            public const string JlcConcept = "JLC Concept";
        }
    }
}
