using System;
using BausCode.Api.Models;
using BausCode.Api.Models.Test;
using Funq;
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
    public class InventoryHandler
    {
        private static readonly Container Container = new Container();

        [OneTimeSetUp]
        public static void FixtureSetup()
        {
            Container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));
            Container.Register(c => c.Resolve<IDbConnectionFactory>().Open());
            Container.Register(Constants.UnitTestUserSession);
            Container.RegisterAutoWired<BausCode.Api.Handlers.InventoryHandler>();

            using (var db = Container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<InventoryTransaction>();
            }
        }

        [Test]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        public void QuantityOnHand_InvalidProductId_Throws(int testId)
        {
            var handler = Container.Resolve<BausCode.Api.Handlers.InventoryHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetQuantityOnHand(testId));
        }
    }
}