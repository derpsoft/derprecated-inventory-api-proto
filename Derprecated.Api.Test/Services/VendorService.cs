namespace Derprecated.Api.Test.Services
{
    using System.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Models.Routing;
    using Models.Test;
    using NUnit.Framework;
    using ServiceStack;
    using ServiceStack.OrmLite;
    using Assert = NUnit.Framework.Assert;

    [TestFixture(
        Description =
            "Tests the happy-path CRUD operations for a vendor."
        )]
    [Author(Constants.Authors.James)]
    internal class VendorService
    {
        private const string BaseUri = "http://localhost:2001/";
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = new TestAppHost("Test::VendorService")
                .Init()
                .Start(BaseUri);

            using (var db = Host.Resolve<IDbConnection>())
            {
                db.Insert(new Vendor {Name = Constants.Vendors.JlcConcept});
            }
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            Host.Dispose();
        }

        [Test]
        [TestOf(typeof (Api.Services.VendorService))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void Vendor_HappyPath_CanCRUD()
        {
            VendorResponse resp = null;
            var client = new JsonServiceClient(BaseUri);
            var login = client.Post(Constants.TestAdminAuthenticate);

            client.SessionId = login.SessionId;

            Assert.DoesNotThrow(() => resp = client.Post(new CreateVendor {Name = Constants.Vendors.JlcConcept}));
            Assert.NotNull(resp);
            Assert.NotNull(resp.Vendor);
            Assert.AreEqual(resp.Vendor.Name, Constants.Vendors.JlcConcept);

            Vendor stored = null;
            using (var db = Host.Container.Resolve<IDbConnection>())
            {
                Assert.DoesNotThrow(() => stored = db.Single(db.From<Vendor>()
                                                               .Where(x => x.Id == resp.Vendor.Id)));
            }

            resp = null;
            Assert.DoesNotThrow(() => resp = client.Get(new GetVendor {Id = 1}));
            Assert.NotNull(resp);
            Assert.NotNull(resp.Vendor);
            Assert.GreaterOrEqual(resp.Vendor.Id, 1);
            Assert.AreEqual(resp.Vendor.Name, Constants.Vendors.JlcConcept);
        }
    }
}
