using System.Data;
using BausCode.Api.Models;
using BausCode.Api.Models.Routing;
using BausCode.Api.Models.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.OrmLite;
using Assert = NUnit.Framework.Assert;
// ReSharper disable once RedundantUsingDirective
using BCH = BausCode.Api.Handlers;
// ReSharper disable once RedundantUsingDirective
using BCS = BausCode.Api.Services;

namespace Derprecated.Api.Test.Services
{
    [TestFixture(
        Description =
            "Tests the full happy-path create, update, and delete of a product that does not exist in any coordinated system."
        )]
    [Author(Constants.Authors.James)]
    internal class ProductService
    {
        private const string BaseUri = "http://localhost:2001/";
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = new TestAppHost("Test::ProductService")
                .Init()
                .Start(BaseUri);
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            Host.Dispose();
        }

        [Test]
        [TestOf(typeof (BCH.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void CreateProduct_HappyPath_Creates()
        {
            SaveProductResponse resp = null;
            var client = new JsonServiceClient(BaseUri);

            var newProduct = new Product
            {
                Title = "Happy path test product",
                Description = "Happy path test product",
                Tags = "test",
                Price = 1000000000000m,
                Sku = "TEST",
                Grams = 1,
                Barcode = "TEST",
                Weight = 1m,
                WeightUnit = "kg",
                Color = "Pink",
                Vendor = Constants.Vendors.JlcConcept
            };

            var login = client.Post(new Authenticate
            {
                provider = AuthenticateService.CredentialsProvider,
                UserName = "test@derprecated.com",
                Password = "123456"
            });

            client.SessionId = login.SessionId;

            Assert.DoesNotThrow(() => resp = client.Post(new SaveProduct {Product = newProduct}));
            Assert.NotNull(resp);
            Assert.NotNull(resp.Product);
            Assert.True(resp.Product.ShopifyId.HasValue);
            Assert.GreaterOrEqual(resp.Product.ShopifyId, 1);
            Assert.AreEqual(resp.Product.Title, newProduct.Title);

            Product stored = null;
            using (var db = Host.Container.Resolve<IDbConnection>())
            {
                Assert.DoesNotThrow(() => stored = db.Single(db.From<Product>()
                    .Where(x => x.ShopifyId == resp.Product.ShopifyId)));
            }
            Assert.NotNull(stored);
            Assert.GreaterOrEqual(stored.ShopifyId, 1);
            Assert.AreEqual(stored.ShopifyId, resp.Product.ShopifyId);
        }

        [Test]
        [TestOf(typeof (BCH.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void DeleteProduct_HappyPath_Deletes()
        {
        }

        [Test]
        [TestOf(typeof (ProductService))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateProduct_HappyPath_Updates()
        {
        }
    }
}