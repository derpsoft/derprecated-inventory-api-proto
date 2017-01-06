namespace Derprecated.Api.Test.Core.Handlers
{
    using System;
    using Models;
    using Models.Attributes;
    using Models.Test;
    using NUnit.Framework;
    using ServiceStack;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using Product = Models.Test.Seeds.Product;

    [TestFixture]
    [Author(Constants.Authors.James)]
    public class ProductHandler
    {
        private ServiceStackHost Host { get; set; }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            //Start your AppHost on TestFixture SetUp
            Host = ServiceStackHost.Instance;

            Host.Container.RegisterAutoWired<Api.Handlers.ProductHandler>();

            using (var db = Host.Resolve<IDbConnectionFactory>().Open())
            {
                db.InsertAll(Product.Basic);
            }
        }

        [Test]
        [TestOf(typeof (Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void Count_GetsCorrectCount()
        {
            var handler = Host.Resolve<Api.Handlers.ProductHandler>();
            long count = -1;

            Assert.DoesNotThrow(() => { count = handler.Count(); });

            Assert.AreEqual((long) Product.Basic.Count, count);
        }

        [Test]
        [TestOf(typeof (Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void GetProduct_WithId_GetsCorrectProduct()
        {
            var handler = Host.Resolve<Api.Handlers.ProductHandler>();
            var expected = Product.EmptyProduct;
            var testId = expected.Id;

            var product = handler.GetProduct(testId);

            Assert.AreEqual(expected.Id, product.Id);
        }

        [Test]
        [TestOf(typeof (Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void GetProduct_WithInvalidId_Throws(int testId)
        {
            var handler = Host.Resolve<Api.Handlers.ProductHandler>();

            Assert.Throws<ArgumentOutOfRangeException>(() => handler.GetProduct(testId));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(int.MaxValue)]
        public void GetProduct_WithValidId_DoesNotThrow(int testId)
        {
            var handler = Host.Resolve<Api.Handlers.ProductHandler>();

            Assert.DoesNotThrow(() => handler.GetProduct(testId));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void SaveProduct_WithExistingProduct_Updates()
        {
            var handler = Host.Resolve<Api.Handlers.ProductHandler>();
            Models.Product result = null;

            Assert.DoesNotThrow(() => handler.Save(Product.EmptyProduct));
            Assert.DoesNotThrow(() => result = handler.GetProduct(Product.EmptyProduct.Id));
            Assert.DoesNotThrow(
                () =>
                    Product.EmptyProduct
                           .ThrowIfInequivalentWithAttribute<Models.Product, EqualityCheckAttribute>(result));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void SaveProduct_WithInvalidExistingProduct_Throws()
        {
            var handler = Host.Resolve<Api.Handlers.ProductHandler>();
            var nonExistentProduct = new Models.Product {Id = int.MaxValue};

            Assert.Throws<ArgumentException>(() => handler.Save(nonExistentProduct));
        }


        [Test]
        [TestOf(typeof (Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void SaveProduct_WithNewProduct_Creates()
        {
            var handler = Host.Resolve<Api.Handlers.ProductHandler>();
            Models.Product result = null;
            var newProduct = new Models.Product
            {
                Title = "New product from test",
                Description = "New product from test description"
            };

            Assert.DoesNotThrow(() => newProduct = handler.Save(newProduct));
            Assert.GreaterOrEqual(newProduct.Id, 1);
            Assert.DoesNotThrow(() => result = handler.GetProduct(newProduct.Id));
            Assert.DoesNotThrow(
                () =>
                    newProduct.ThrowIfInequivalentWithAttribute<Models.Product, EqualityCheckAttribute>(
                        result));
        }

        [Test]
        [TestOf(typeof (Api.Handlers.ProductHandler))]
        [Author(Constants.Authors.James)]
        public void SaveProduct_WithNullProduct_Throws()
        {
            var handler = Host.Resolve<Api.Handlers.ProductHandler>();

            Assert.Throws<ArgumentNullException>(() => handler.Save(null));
        }
    }
}
