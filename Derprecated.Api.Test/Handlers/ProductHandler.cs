using System;
using BausCode.Api.Models;
using BausCode.Api.Models.Test;
using Funq;
using NUnit.Framework;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Derprecated.Api.Test.Handlers
{
    [TestFixture]
    [Parallelizable]
    [Author(Constants.Authors.James)]
    public class ProductHandler
    {
        private static readonly Container Container = new Container();

        [OneTimeSetUp]
        public static void FixtureSetup()
        {
            Container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));
            Container.Register(c => c.Resolve<IDbConnectionFactory>().Open());
            Container.Register(Constants.UnitTestUserSession);
            Container.RegisterAutoWired<BausCode.Api.Handlers.ProductHandler>();

            using (var db = Container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Product>();
                db.DropAndCreateTable<ProductImage>();
                db.InsertAll(BausCode.Api.Models.Test.Seeds.Product.Basic);
            }
        }

        [Test]
        [TestOf(typeof(BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void Count_GetsCorrectCount()
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.ProductHandler>();
            long count = -1;

            Assert.DoesNotThrow(() => { count = handler.Count(); });

            Assert.AreEqual((long) BausCode.Api.Models.Test.Seeds.Product.Basic.Count, count);
        }

        [Test]
        [TestOf(typeof(BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void GetProduct_WithId_GetsCorrectProduct()
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.ProductHandler>();
            var expected = BausCode.Api.Models.Test.Seeds.Product.EmptyProduct;
            var testId = expected.Id;

            var product = handler.GetProduct(testId);

            Assert.AreEqual(expected.Id, product.Id);
        }

        [Test]
        [TestOf(typeof(BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void GetProduct_WithInvalidId_Throws(int testId)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.ProductHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetProduct(testId));
        }

        [Test]
        [TestOf(typeof(BausCode.Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(int.MaxValue)]
        public void GetProduct_WithValidId_DoesNotThrow(int testId)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.ProductHandler>();

            Assert.DoesNotThrow(() => handler.GetProduct(testId));
        }
    }
}