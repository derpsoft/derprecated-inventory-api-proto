namespace Derprecated.Api.Test.Core.Handlers
{
    using System;
    using Models.Test;
    using Models.Test.Seeds;
    using NUnit.Framework;
    using ServiceStack;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    [TestFixture]
    [Author(Constants.Authors.James)]
    public class InventoryHandler
    {
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = ServiceStackHost.Instance;

            Host.Container.RegisterAutoWired<Api.Handlers.InventoryHandler>();

            using (var db = Host.Resolve<IDbConnectionFactory>().Open())
            {
                db.SaveAll(Product.Basic);
                db.SaveAll(Location.Basic);
            }
        }

        [Test]
        [TestOf(typeof (Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void QuantityOnHand_InvalidProductId_Throws(int testId)
        {
            var handler = Host.Resolve<Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetQuantityOnHand(testId));
        }

        [Test]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Receive_InvalidLocationId_Throws(int testId)
        {
            var handler = Host.Resolve<Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () => handler.Receive(Product.EmptyProduct.Id, testId, 1));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Receive_InvalidProductId_Throws(int testId)
        {
            var handler = Host.Resolve<Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () => handler.Receive(testId, Location.EmptyLocation.Id, 1));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Receive_InvalidQuantity_Throws(decimal testQuantity)
        {
            var handler = Host.Resolve<Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    handler.Receive(Product.EmptyProduct.Id,
                        Location.EmptyLocation.Id, testQuantity));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(3475)]
        public void Receive_LocationNotFound_Throws(int testId)
        {
            var handler = Host.Resolve<Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentNullException>(() => handler.Receive(Product.EmptyProduct.Id, testId, 1m));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(3475)]
        public void Receive_ProductNotFound_Throws(int testId)
        {
            var handler = Host.Resolve<Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentNullException>(() => handler.Receive(testId, Location.EmptyLocation.Id, 1m));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.InventoryHandler))]
        [Author(Constants.Authors.James)]
        public void Receive_ValidData_CreatesRecordAndIncrementsQuantity()
        {
            var handler = Host.Resolve<Api.Handlers.InventoryHandler>();
            var testQuantity = 1;
            var observedQuantity = -1m;

            Assert.DoesNotThrow(
                () => handler.Receive(Product.EmptyProduct.Id, Location.EmptyLocation.Id, testQuantity));
            Assert.AreNotEqual(observedQuantity, testQuantity);
            Assert.DoesNotThrow(() => observedQuantity = handler.GetQuantityOnHand(Product.EmptyProduct.Id));
            Assert.AreEqual(testQuantity, observedQuantity);
        }
    }
}
