using System;
using BausCode.Api.Models.Shopify;
using BausCode.Api.Models.Test;
using Funq;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Configuration;
// ReSharper disable once RedundantUsingDirective
using Seeds = BausCode.Api.Models.Test.Seeds;
// ReSharper disable once RedundantUsingDirective
using BC = BausCode.Api.Handlers;

namespace Derprecated.Api.Test.Handlers
{
    [TestFixture]
    [Parallelizable]
    [Author(Constants.Authors.James)]
    public class ShopifyHandler
    {
        private static readonly Container Container = new Container();

        [OneTimeSetUp]
        public static void FixtureSetup()
        {
            var appSettings = new AppSettings();

            Container.Register(new JsonServiceClient($"https://{appSettings.Get("shopify.store.domain")}")
            {
                UserName = appSettings.Get("shopify.api.key"),
                Password = appSettings.Get("shopify.api.password")
            });

            Container.RegisterAutoWired<BC.ShopifyHandler>();
        }

        [Test]
        [TestOf(typeof(BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetProduct_InvalidId_Throws(long testId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetProduct(testId));
        }

        [Test]
        [TestOf(typeof(BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(Constants.Shopify.IntegrationTestProductId)]
        public void GetProduct_ValidId_GetsProduct(long testId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Product product = null;

            Assert.DoesNotThrow(() => product = handler.GetProduct(testId));
            Assert.AreEqual(testId, product.Id);
        }

        [Test]
        [TestOf(typeof(BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0, 1)]
        [TestCase(-1, 1)]
        public void GetImage_InvalidProductId_Throws(long productId, long imageId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetImage(productId, imageId));
        }

        [Test]
        [TestOf(typeof(BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(1, 0)]
        [TestCase(1, -1)]
        public void GetImage_InvalidImageId_Throws(long productId, long imageId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetImage(productId, imageId));
        }

        [Test]
        [TestOf(typeof(BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(Constants.Shopify.IntegrationTestProductId, Constants.Shopify.IntegrationTestImageId)]
        public void GetImage_ValidIds_GetsImage(long productId, long imageId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Image image = null;

            Assert.DoesNotThrow(() => image = handler.GetImage(productId, imageId));
            Assert.AreEqual(imageId, image.Id);
        }


        [Test]
        [TestOf(typeof(BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0, 1)]
        [TestCase(-1, 1)]
        public void GetVariant_InvalidProductId_Throws(long productId, long variantId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetVariant(productId, variantId));
        }

        [Test]
        [TestOf(typeof(BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(1, 0)]
        [TestCase(1, -1)]
        public void GetVariant_InvalidImageId_Throws(long productId, long variantId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetVariant(productId, variantId));
        }

        [Test]
        [TestOf(typeof(BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(Constants.Shopify.IntegrationTestProductId, Constants.Shopify.IntegrationTestVariantId)]
        public void GetVariant_ValidIds_GetsVariant(long productId, long variantId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Variant image = null;

            Assert.DoesNotThrow(() => image = handler.GetVariant(productId, variantId));
            Assert.AreEqual(variantId, image.Id);
        }
    }
}