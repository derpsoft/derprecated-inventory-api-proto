namespace Derprecated.Api.Test.Services
{
    using System;
    using BausCode.Api.Models.Routing;
    using BausCode.Api.Models.Test;
    using BausCode.Api.Models.Test.Seeds;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnit.Framework;
    using ServiceStack;
    using BCS = BausCode.Api.Services;

    [TestFixture(
        Description =
            "Tests the full happy-path to increment and decrement inventory for a product."
        )]
    public class InventoryService
    {
        private const string BaseUri = "http://localhost:2001/";
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = new TestAppHost("Test::InventoryService")
                .Init()
                .Start(BaseUri);
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            Host.Dispose();
        }

        [Test]
        [TestOf(typeof (BCS.InventoryService))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void ReceiveQuantity_HappyPath_Receives()
        {
            CreateInventoryTransactionResponse resp = null;

            var rng = new Random();
            var quant = rng.Next(10) + 1;
            var client = new JsonServiceClient(BaseUri);
            var xact = new CreateInventoryTransaction
                       {
                           ItemId = Product.Light.Id,
                           LocationId = Location.TestRack.Id,
                           Quantity = quant
                       };
        }
    }
}
