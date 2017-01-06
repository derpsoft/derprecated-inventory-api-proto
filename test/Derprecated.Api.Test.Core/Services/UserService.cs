namespace Derprecated.Api.Test.Core.Services
{
    using System;
    using Models;
    using Models.Routing;
    using Models.Test;
    using NUnit.Framework;
    using ServiceStack;

    [TestFixture(
        Description =
            "Tests the happy-path get for a user."
        )]
    [Author(Constants.Authors.James)]
    internal class UserService
    {
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = ServiceStackHost.Instance;
        }

        [Test]
        [TestOf(typeof (Api.Services.UserService))]
        [Author(Constants.Authors.James)]
        [Category(Constants.Categories.Integration)]
        public void GetUser_RequiresAuth()
        {
            var client = new JsonServiceClient(TestAppHost.BaseUri);
            var exception = Assert.Throws<WebServiceException>(() => client.Get(new GetUser {Id = 10000}));
            Assert.AreEqual(401, exception.StatusCode);
        }

        [Test]
        [TestOf(typeof (Api.Services.UserService))]
        [Author(Constants.Authors.James)]
        [Category(Constants.Categories.Integration)]
        [TestCase(Roles.Admin)]
        public void GetUser_RequiresRole(string roleName)
        {
            var client = new JsonServiceClient(TestAppHost.BaseUri);
            var login = client.Post(Constants.TestAuthenticate);
            client.SessionId = login.SessionId;

            var exception = Assert.Throws<WebServiceException>(() => client.Get(new GetUser {Id = 10000}));
            Assert.AreEqual(403, exception.StatusCode);
            Assert.AreEqual("Invalid Role", exception.Message);
        }


        [Test]
        [TestOf(typeof (Api.Services.UserService))]
        [Author(Constants.Authors.James)]
        [Category(Constants.Categories.Integration)]
        public void User_HappyPath_CanRUD()
        {
            var rng = new Random();
            GetUserResponse resp = null;
            var client = new JsonServiceClient(TestAppHost.BaseUri);
            var login = client.Post(Constants.TestAdminAuthenticate);
            client.SessionId = login.SessionId;

            Assert.DoesNotThrow(() => resp = client.Get(new GetUser {Id = 1}));
            Assert.NotNull(resp);
            Assert.NotNull(resp.User);
            Assert.GreaterOrEqual(resp.User.Id, 1);
            Assert.AreEqual(resp.User.Email, Constants.TestAuthenticate.UserName);

            var newPhone = rng.Next(1, 100000).ToString();
            Assert.AreNotEqual(resp.User.PhoneNumber, newPhone);
            resp = null;
            Assert.DoesNotThrow(() => resp = client.Put(new UpdateUser {Id = 1, PhoneNumber = newPhone}));
            Assert.NotNull(resp);
            Assert.NotNull(resp.User);
            Assert.AreEqual(resp.User.Id, 1);
            Assert.AreEqual(resp.User.PhoneNumber, newPhone);
        }
    }
}
