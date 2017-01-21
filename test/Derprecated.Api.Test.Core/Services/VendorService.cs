namespace Derprecated.Api.Test.Core.Services
{
    using System.Data;
    using Models.Dto;
    using Models.Routing;
    using Models.Test;
    using NUnit.Framework;
    using ServiceStack;
    using ServiceStack.OrmLite;
    using Vendor = Models.Vendor;

    [TestFixture(
        Description =
            "Tests the happy-path CRUD operations for a vendor."
        )]
    [Author(Constants.Authors.James)]
    internal class VendorService
    {
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            Host = ServiceStackHost.Instance;

            using (var db = Host.Resolve<IDbConnection>())
            {
                db.Insert(new Vendor {Name = Constants.Vendors.JlcConcept});
            }
        }

        [Test]
        [TestOf(typeof (Api.Services.VendorService))]
        [Author(Constants.Authors.James)]
        [Category(Constants.Categories.Integration)]
        public void Vendor_HappyPath_CanCRUD()
        {
            Dto<Models.Dto.Vendor> resp = null;
            var client = new JsonServiceClient(TestAppHost.BaseUri);
            var login = client.Post(Constants.TestAdminAuthenticate);

            client.SessionId = login.SessionId;

            Assert.DoesNotThrow(
                () => resp = client.Post(new Models.Dto.Vendor {Name = Constants.Vendors.JlcConcept}));
            Assert.NotNull(resp);
            Assert.NotNull(resp.Result);
            Assert.AreEqual(resp.Result.Name, Constants.Vendors.JlcConcept);

            Vendor stored = null;
            using (var db = Host.Container.Resolve<IDbConnection>())
            {
                Assert.DoesNotThrow(() => stored = db.Single(db.From<Vendor>()
                                                               .Where(x => x.Id == resp.Result.Id)));
            }

            resp = null;
            Assert.DoesNotThrow(() => resp = client.Get(new Models.Dto.Vendor {Id = 1}));
            Assert.NotNull(resp);
            Assert.NotNull(resp.Result);
            Assert.GreaterOrEqual(resp.Result.Id, 1);
            Assert.AreEqual(resp.Result.Name, Constants.Vendors.JlcConcept);
        }
    }
}
