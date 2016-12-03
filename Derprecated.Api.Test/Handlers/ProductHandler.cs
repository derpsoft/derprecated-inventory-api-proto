using System;
using BausCode.Api.Models;
using BausCode.Api.Models.Attributes;
using BausCode.Api.Models.Test;
using Funq;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
// ReSharper disable once RedundantUsingDirective
using Seeds = BausCode.Api.Models.Test.Seeds;

namespace Derprecated.Api.Test.Handlers
{
    [TestFixture]
    [Parallelizable]
    [Author(Constants.Authors.James)]
    public class ProductHandler
    {
        private const string BaseUri = "http://localhost:2001/";
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = new TestAppHost("Test::ProductHandler")
                .Init()
                .Start(BaseUri);

            Host.Container.RegisterAutoWired<BausCode.Api.Handlers.ProductHandler>();

            using (var db = Host.Resolve<IDbConnectionFactory>().Open())
            {
                db.InsertAll(Seeds.Product.Basic);
            }
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            Host.Dispose();
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void Count_GetsCorrectCount()
        {
            var handler = Host.Resolve<BausCode.Api.Handlers.ProductHandler>();
            long count = -1;

            Assert.DoesNotThrow(() => { count = handler.Count(); });

            Assert.AreEqual((long) Seeds.Product.Basic.Count, count);
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void GetProduct_WithId_GetsCorrectProduct()
        {
            var handler = Host.Resolve<BausCode.Api.Handlers.ProductHandler>();
            var expected = Seeds.Product.EmptyProduct;
            var testId = expected.Id;

            var product = handler.GetProduct(testId);

            Assert.AreEqual(expected.Id, product.Id);
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void GetProduct_WithInvalidId_Throws(int testId)
        {
            var handler = Host.Resolve<BausCode.Api.Handlers.ProductHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetProduct(testId));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(int.MaxValue)]
        public void GetProduct_WithValidId_DoesNotThrow(int testId)
        {
            var handler = Host.Resolve<BausCode.Api.Handlers.ProductHandler>();

            Assert.DoesNotThrow(() => handler.GetProduct(testId));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void SaveProduct_WithExistingProduct_Updates()
        {
            var handler = Host.Resolve<BausCode.Api.Handlers.ProductHandler>();
            Product result = null;

            Assert.DoesNotThrow(() => handler.Save(Seeds.Product.EmptyProduct));
            Assert.DoesNotThrow(() => result = handler.GetProduct(Seeds.Product.EmptyProduct.Id));
            Assert.DoesNotThrow(
                () =>
                    Seeds.Product.EmptyProduct.ThrowIfInequivalentWithAttribute<Product, EqualityCheckAttribute>(result));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void SaveProduct_WithInvalidExistingProduct_Throws()
        {
            var handler = Host.Resolve<BausCode.Api.Handlers.ProductHandler>();
            var nonExistentProduct = new Product {Id = int.MaxValue};

            Assert.Throws<ArgumentException>(() => handler.Save(nonExistentProduct));
        }


        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void SaveProduct_WithNewProduct_Creates()
        {
            var handler = Host.Resolve<BausCode.Api.Handlers.ProductHandler>();
            Product result = null;
            var newProduct = new Product
            {
                Title = "New product from test",
                Description = "New product from test description"
            };

            Assert.DoesNotThrow(() => newProduct = handler.Save(newProduct));
            Assert.GreaterOrEqual(newProduct.Id, 1);
            Assert.DoesNotThrow(() => result = handler.GetProduct(newProduct.Id));
            Assert.DoesNotThrow(
                () => newProduct.ThrowIfInequivalentWithAttribute<Product, EqualityCheckAttribute>(result));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void SaveProduct_WithNullProduct_Throws()
        {
            var handler = Host.Resolve<BausCode.Api.Handlers.ProductHandler>();

            Assert.Throws<ArgumentNullException>(() => handler.Save(null));
        }
    }
}