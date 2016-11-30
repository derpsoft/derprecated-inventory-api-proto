using System;
using BausCode.Api.Models;
using BausCode.Api.Models.Test;
using Funq;
using NUnit.Framework;
using ServiceStack.Data;
using ServiceStack.OrmLite;
// ReSharper disable once RedundantUsingDirective
using Seeds = BausCode.Api.Models.Test.Seeds;

namespace Derprecated.Api.Test.Handlers
{
    [TestFixture]
    [Parallelizable]
    [Author(Constants.Authors.James)]
    public class InventoryHandler
    {
        private static readonly Container Container = new Container();

        [OneTimeSetUp]
        public static void FixtureSetup()
        {
            Container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));
            Container.Register(c => c.Resolve<IDbConnectionFactory>().Open());
            Container.Register(Constants.TestUserSession);
            Container.RegisterAutoWired<BausCode.Api.Handlers.InventoryHandler>();

            using (var db = Container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Product>();
                db.DropAndCreateTable<ProductImage>();
                db.DropAndCreateTable<Location>();
                db.DropAndCreateTable<InventoryTransaction>();

                db.SaveAll(Seeds.Product.Basic);
                db.SaveAll(Seeds.Location.Basic);
            }
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void QuantityOnHand_InvalidProductId_Throws(int testId)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetQuantityOnHand(testId));
        }

        [Test]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Receive_InvalidLocationId_Throws(int testId)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () => handler.Receive(Seeds.Product.EmptyProduct.Id, testId, 1));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Receive_InvalidProductId_Throws(int testId)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () => handler.Receive(testId, Seeds.Location.EmptyLocation.Id, 1));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Receive_InvalidQuantity_Throws(decimal testQuantity)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    handler.Receive(Seeds.Product.EmptyProduct.Id,
                        Seeds.Location.EmptyLocation.Id, testQuantity));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(3475)]
        public void Receive_LocationNotFound_Throws(int testId)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentNullException>(() => handler.Receive(Seeds.Product.EmptyProduct.Id, testId, 1m));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(3475)]
        public void Receive_ProductNotFound_Throws(int testId)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentNullException>(() => handler.Receive(testId, Seeds.Location.EmptyLocation.Id, 1m));
        }

        [Test]
        [TestOf(typeof (BausCode.Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        public void Receive_ValidData_CreatesRecordAndIncrementsQuantity()
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.InventoryHandler>();
            var testQuantity = 1;
            var observedQuantity = -1m;

            Assert.DoesNotThrow(() => handler.Receive(Seeds.Product.EmptyProduct.Id, Seeds.Location.EmptyLocation.Id, testQuantity));
            Assert.AreNotEqual(observedQuantity, testQuantity);
            Assert.DoesNotThrow(() => observedQuantity = handler.GetQuantityOnHand(Seeds.Product.EmptyProduct.Id));
            Assert.AreEqual(testQuantity, observedQuantity);
        }
    }
}