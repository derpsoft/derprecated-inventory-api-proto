namespace Derprecated.Api.Test.Services
{
    using BausCode.Api.Models;
    using BausCode.Api.Models.Routing;
    using BausCode.Api.Models.Test;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnit.Framework;
    using ServiceStack;
    using Assert = NUnit.Framework.Assert;
    using BCS = BausCode.Api.Services;

    [TestFixture(
        Description =
            "Tests the happy-path get for a user."
        )]
    [Author(Constants.Authors.James)]
    internal class UserService
    {
        private const string BaseUri = "http://localhost:2001/";
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = new TestAppHost("Test::UserService")
                .Init()
                .Start(BaseUri);
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            Host.Dispose();
        }


        [Test]
        [TestOf(typeof(BCS.UserService))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void User_HappyPath_CanGet()
        {
            GetUserResponse resp = null;
            var client = new JsonServiceClient(BaseUri);
            var login = client.Post(Constants.TestAdminAuthenticate);

            client.SessionId = login.SessionId;

            Assert.DoesNotThrow(() => resp = client.Get(new GetUser { Id = 1 }));
            Assert.NotNull(resp);
            Assert.NotNull(resp.User);
            Assert.GreaterOrEqual(resp.User.Id, 1);
            Assert.AreEqual(resp.User.Email, Constants.TestAuthenticate.UserName);
        }

        [Test]
        [TestOf(typeof(BCS.UserService))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void GetUser_RequiresAuth()
        {
            var client = new JsonServiceClient(BaseUri);
            var exception = Assert.Throws<WebServiceException>(() => client.Get(new GetUser { Id = 10000 }));
            Assert.AreEqual(401, exception.StatusCode);
        }

        [Test]
        [TestOf(typeof(BCS.UserService))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        [TestCase(Roles.Admin)]
        public void GetUser_RequiresRole(string roleName)
        {
            var client = new JsonServiceClient(BaseUri);
            var login = client.Post(Constants.TestAuthenticate);
            client.SessionId = login.SessionId;

            var exception = Assert.Throws<WebServiceException>(() => client.Get(new GetUser { Id = 10000 }));
            Assert.AreEqual(403, exception.StatusCode);
            Assert.AreEqual("Invalid Role", exception.Message);
        }

    }
}
