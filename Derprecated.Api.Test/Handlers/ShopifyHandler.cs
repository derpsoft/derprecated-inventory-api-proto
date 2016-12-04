namespace Derprecated.Api.Test.Handlers
{
    using System;
    using System.Collections.Generic;
    using BausCode.Api.Models.Shopify;
    using BausCode.Api.Models.Test;
    using Funq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnit.Framework;
    using ServiceStack;
    using ServiceStack.Configuration;
    using Assert = NUnit.Framework.Assert;
    using BC = BausCode.Api.Handlers;

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
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        public void CreateProduct_ValidProduct_Creates()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            var rng = new Random();
            Product result = null;
            var nonce = rng.Next(100) + 1;
            var newProduct = new Product
                             {
                                 Title = $"ShopifyHandler test product {nonce}",
                                 Tags = "test",
                                 BodyHtml = $"TEST PRODUCT. DO NOT SELL. TEST DATA FOLLOWS<br><br>{nonce}",
                                 IsPublished = false,
                                 Variants = new List<Variant>
                                            {
                                                new Variant
                                                {
                                                    Price = $"{nonce}000000000000.00",
                                                    Sku = "TEST",
                                                    Barcode = "TEST"
                                                }
                                            }
                             };

            Assert.DoesNotThrow(() => result = handler.Create(newProduct));
            Assert.NotNull(result);
            Assert.That(result.Title.Equals(newProduct.Title));
            Assert.That(result.BodyHtml.Equals(newProduct.BodyHtml));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(1, 0)]
        [TestCase(1, -1)]
        public void GetImage_InvalidImageId_Throws(long productId, long imageId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetImage(productId, imageId));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0, 1)]
        [TestCase(-1, 1)]
        public void GetImage_InvalidProductId_Throws(long productId, long imageId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetImage(productId, imageId));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
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
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetProduct_InvalidId_Throws(long testId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetProduct(testId));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
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
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(1, 0)]
        [TestCase(1, -1)]
        public void GetVariant_InvalidImageId_Throws(long productId, long variantId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetVariant(productId, variantId));
        }


        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0, 1)]
        [TestCase(-1, 1)]
        public void GetVariant_InvalidProductId_Throws(long productId, long variantId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetVariant(productId, variantId));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(Constants.Shopify.IntegrationTestProductId, Constants.Shopify.IntegrationTestVariantId)]
        public void GetVariant_ValidIds_GetsVariant(long productId, long variantId)
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Variant image = null;

            Assert.DoesNotThrow(() => image = handler.GetVariant(productId, variantId));
            Assert.AreEqual(variantId, image.Id);
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateImage_InvalidId_Throws()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Assert.Throws<ArgumentNullException>(() => handler.Update(new Image {Id = null}));
            Assert.Throws<ArgumentOutOfRangeException>(() => handler.Update(new Image {Id = -1}));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateImage_InvalidProductId_Throws()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Assert.Throws<ArgumentNullException>(() => handler.Update(new Image {Id = 1, ProductId = null}));
            Assert.Throws<ArgumentOutOfRangeException>(() => handler.Update(new Image {Id = 1, ProductId = -1}));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateImage_NullImage_Throws()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Assert.Throws<ArgumentNullException>(() => handler.Update(default(Image)));
            Assert.Throws<ArgumentNullException>(() => handler.Update((Image) null));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateImage_ValidImage_Updates()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            var update = new Image
                         {
                             Id = Constants.Shopify.IntegrationTestImageId,
                             ProductId = Constants.Shopify.IntegrationTestProductId,
                             VariantIds = new List<long> {Constants.Shopify.IntegrationTestVariantId},
                             Url = "https://unsplash.it/500/500/?random"
                         };
            Image result = null;

            Assert.DoesNotThrow(() => result = handler.Update(update));
            Assert.That(result.Url.Contains("cdn.shopify"));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        public void UpdateProduct_InvalidId_Throws()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Assert.Throws<ArgumentNullException>(() => handler.Update(new Product {Id = null}));
            Assert.Throws<ArgumentOutOfRangeException>(() => handler.Update(new Product {Id = -1}));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateProduct_NullProduct_Throws()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Assert.Throws<ArgumentNullException>(() => handler.Update(default(Product)));
            Assert.Throws<ArgumentNullException>(() => handler.Update((Product) null));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateProduct_ValidProduct_Updates()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            var nonce = new Random().Next(100);
            var update = new Product
                         {
                             Id = Constants.Shopify.IntegrationTestProductId,
                             BodyHtml = $"DO NOT DELETE. DO NOT SELL. TEST DATA FOLLOWS.<br><br>{nonce}"
                         };
            Product result = null;

            Assert.DoesNotThrow(() => result = handler.Update(update));
            Assert.That(result.BodyHtml.Equals(update.BodyHtml, StringComparison.InvariantCulture));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateVariant_InvalidId_Throws()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Assert.Throws<ArgumentNullException>(() => handler.Update(new Variant {Id = null}));
            Assert.Throws<ArgumentOutOfRangeException>(() => handler.Update(new Variant {Id = -1}));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateVariant_NullVariant_Throws()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            Assert.Throws<ArgumentNullException>(() => handler.Update(default(Variant)));
            Assert.Throws<ArgumentNullException>(() => handler.Update((Variant) null));
        }

        [Test]
        [TestOf(typeof (BC.ShopifyHandler))]
        [Author(Constants.Authors.James)]
        [TestCategory(Constants.Categories.Integration)]
        public void UpdateVariant_ValidVariant_Updates()
        {
            var handler = Container.Resolve<BC.ShopifyHandler>();
            var nonce = new Random().Next(100) + 1;
            var update = new Variant
                         {
                             Id = Constants.Shopify.IntegrationTestVariantId,
                             Price = $"{nonce}000000000000.00"
                         };
            Variant result = null;

            Assert.DoesNotThrow(() => result = handler.Update(update));
            Assert.That(result.Price.Equals(update.Price, StringComparison.InvariantCulture));
        }
    }
}
