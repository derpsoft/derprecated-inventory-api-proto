namespace Derprecated.Api.Test.Core.Services
{
    using System;
    using System.Data;
    using Models.Routing;
    using Models.Test;
    using Models.Test.Seeds;
    using NUnit.Framework;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.OrmLite;
    using Assert = NUnit.Framework.Assert;

    [TestFixture(
        Description =
            "Tests the full happy-path to increment and decrement inventory for a product."
        )]
    public class InventoryService
    {
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = ServiceStackHost.Instance;

            using (var db = Host.Resolve<IDbConnection>())
            {
                db.Save(Product.Light);
                db.Save(Product.CatTree);
                db.Save(Location.TestRack);
            }
        }

        [Test]
        [TestOf(typeof(Api.Services.InventoryService))]
        [Author(Constants.Authors.James)]
        [Category(Constants.Categories.Integration)]
        public void Receive_RequiresAuth()
        {
            var client = new JsonServiceClient(TestAppHost.BaseUri);
            var exception = Assert.Throws<WebServiceException>(() => client.Post(new CreateInventoryTransaction()));
            Assert.AreEqual(401, exception.StatusCode);
        }

        [Test]
        [TestOf(typeof (Api.Services.InventoryService))]
        [Author(Constants.Authors.James)]
        [Category(Constants.Categories.Integration)]
        public void ReceiveAndReleaseQuantity_HappyPath_ReceivesAndReleases()
        {
            var productId = Product.Light.Id;
            var locationId = Location.TestRack.Id;
            var rng = new Random();
            var quant = rng.Next(10) + 1;
            var client = new JsonServiceClient(TestAppHost.BaseUri);
            var xact = new CreateInventoryTransaction
                       {
                           ProductId = productId,
                           LocationId = locationId,
                           Quantity = quant
                       };
            var count = new GetProductQuantityOnHand {Id = xact.ProductId};

            var login = client.Post(new Authenticate
            {
                provider = AuthenticateService.CredentialsProvider,
                UserName = "test@derprecated.com",
                Password = "123456"
            });
            client.SessionId = login.SessionId;

            client.Post(xact);
            var qoh = client.Get(count);

            Assert.GreaterOrEqual(quant, 1);
            Assert.NotNull(qoh);
            Assert.AreEqual(quant, qoh.Quantity);

            xact.Quantity = -xact.Quantity + 1;

            client.Post(xact);
            qoh = client.Get(count);

            Assert.NotNull(qoh);
            Assert.AreEqual(1, qoh.Quantity);
        }
    }
}
