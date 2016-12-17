namespace Derprecated.Api.Handlers
{
    using System;
    using Models.Shopify;
    using ServiceStack;

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
                throw new ArgumentNullException(nameof(product.Id));
            product.Id.Value.ThrowIfLessThan(1);

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

        public Image Update(Image image)
        {
            image.ThrowIfNull();

            if (!image.Id.HasValue)
                throw new ArgumentNullException(nameof(image.Id));
            image.Id.Value.ThrowIfLessThan(1);

            if (!image.ProductId.HasValue)
                throw new ArgumentNullException(nameof(image.ProductId));
            image.ProductId.Value.ThrowIfLessThan(1);

            var result =
                Client.Put(new UpdateImage {Id = image.Id.Value, ProductId = image.ProductId.Value, Image = image});
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

        public Variant Update(Variant variant)
        {
            variant.ThrowIfNull();

            if (!variant.Id.HasValue)
                throw new ArgumentNullException(nameof(variant.Id));
            variant.Id.Value.ThrowIfLessThan(1);

            var result = Client.Put(new UpdateVariant {Id = variant.Id.Value, Variant = variant});

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
