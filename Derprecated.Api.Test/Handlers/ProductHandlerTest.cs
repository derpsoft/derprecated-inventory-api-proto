using System;
using BausCode.Api.Handlers;
using BausCode.Api.Models;
using BausCode.Api.Models.Test;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;

namespace Derprecated.Api.Test.Handlers
{
    [TestFixture]
    [Parallelizable]
    [Author(Constants.Authors.James)]
    public class ProductHandlerTest
    {
        private static ServiceStackHost AppHost;

        [OneTimeSetUp]
        public static void FixtureSetup()
        {
            AppHost = new BasicAppHost().Init();

            var container = AppHost.Container;


            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));
            container.Register(c => c.Resolve<IDbConnectionFactory>().Open());
            container.Register(Constants.UnitTestUserSession);
            container.RegisterAutoWired<ProductHandler>();

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Product>();
                db.DropAndCreateTable<ProductImage>();
                db.InsertAll(BausCode.Api.Models.Test.Seeds.Product.Basic);
            }
        }

        [Test]
        [Author(Constants.Authors.James)]
        public void CanCountProducts()
        {
            var handler = AppHost.Container.Resolve<ProductHandler>();
            long count = -1;

            Assert.DoesNotThrow(() => { count = handler.Count(); });

            Assert.AreEqual((long) BausCode.Api.Models.Test.Seeds.Product.Basic.Count, count);
        }

        [Test]
        [Author(Constants.Authors.James)]
        public void GetProduct_WithId_GetsCorrectProduct()
        {
            var handler = AppHost.Container.Resolve<ProductHandler>();
            var expected = BausCode.Api.Models.Test.Seeds.Product.EmptyProduct;
            var testId = expected.Id;

            var product = handler.GetProduct(testId);

            Assert.AreEqual(expected.Id, product.Id);
        }

        [Test]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void GetProduct_WithInvalidId_Throws(int testId)
        {
            var handler = AppHost.Container.Resolve<ProductHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetProduct(testId));
        }

        [Test]
        [Author(Constants.Authors.James)]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(int.MaxValue)]
        public void GetProduct_WithValidId_DoesNotThrow(int testId)
        {
            var handler = AppHost.Container.Resolve<ProductHandler>();

            Assert.DoesNotThrow(() => handler.GetProduct(testId));
        }
    }
}