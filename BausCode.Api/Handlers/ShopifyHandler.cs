using System;
using BausCode.Api.Models.Shopify;
using ServiceStack;

namespace BausCode.Api.Handlers
{
    public class ShopifyHandler
    {
        public ShopifyHandler(JsonServiceClient client)
        {
            Client = client;
        }

        private JsonServiceClient Client { get; }

        public Product GetProduct(long id)
        {
            id.ThrowIfLessThan(1);

            var result = Client.Get(new GetProduct {Id = id});

            return result.Product;
        }

        public Product Update(Product product)
        {
            product.ThrowIfNull();

            if (!product.Id.HasValue)
                throw new ArgumentNullException(nameof(product));

            var result = Client.Put(new UpdateProduct {Id = product.Id.Value, Product = product});

            return result.Product;
        }

        public Product Create(Product product)
        {
            product.ThrowIfNull();

            var result = Client.Post(new CreateProduct {Product = product});

            return result.Product;
        }

        public Image GetImage(long productId, long id)
        {
            productId.ThrowIfLessThan(1);
            id.ThrowIfLessThan(1);

            var result = Client.Get(new GetImage {Id = id, ProductId = productId});

            return result.Image;
        }

        public Image Update(UpdateImage image)
        {
            image.ThrowIfNull();

            var result = Client.Put(image);

            return result.Image;
        }

        public Image Create(CreateImage image)
        {
            image.ThrowIfNull();

            var result = Client.Post(image);

            return result.Image;
        }

        public Variant GetVariant(long productId, long id)
        {
            productId.ThrowIfLessThan(1);
            id.ThrowIfLessThan(1);

            var result = Client.Get(new GetVariant {Id = id});

            return result.Variant;
        }

        public Variant Update(UpdateVariant variant)
        {
            variant.ThrowIfNull();

            var result = Client.Put(variant);

            return result.Variant;
        }

        public Variant Create(CreateVariant variant)
        {
            variant.ThrowIfNull();

            var result = Client.Post(variant);

            return result.Variant;
        }
    }
}