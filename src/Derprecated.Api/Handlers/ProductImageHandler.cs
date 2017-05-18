namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    public class ProductImageHandler : CrudHandler<ProductImage>
    {
        public ProductImageHandler(IDbConnectionFactory db) : base(db)
        {
        }

        public override List<ProductImage> Typeahead(string query, bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(int imageId, bool includeDeleted = false)
        {
            var q = Db.From<ProductImage>()
                      .Join<Product>((productImage, product) => productImage.ProductId == product.Id)
                      .Where<ProductImage>(x => x.ImageId == imageId);

            if (!includeDeleted)
                q = q.Where<ProductImage>(x => !x.IsDeleted)
                     .Where<Product>(x => !x.IsDeleted);


            return Db.Select<Product>(q);
        }

        public List<Image> GetImages(int productId, bool includeDeleted = false)
        {
            var q = Db.From<ProductImage>()
                      .Join<Image>((productImage, image) => productImage.ImageId == image.Id)
                      .Where<ProductImage>(x => x.ProductId == productId);

            if (!includeDeleted)
                q = q.Where<Image>(x => !x.IsDeleted);

            return Db.Select<Image>(q);
        }

        public List<int> AssociateProducts(int imageId, List<int> productIds)
        {
            using (var trans = Db.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                Db.Delete<ProductImage>(x => x.ImageId == imageId);
                Db.SaveAll(productIds.Select(x => new ProductImage
                {
                    ImageId = imageId,
                    ProductId = x
                }));
                trans.Commit();
            }
            return productIds;
        }

        public List<int> AssociateImages(int productId, List<int> imageIds)
        {
            using (var trans = Db.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                Db.Delete<ProductImage>(x => x.ProductId == productId);
                Db.SaveAll(imageIds.Select(x => new ProductImage
                {
                    ImageId = x,
                    ProductId = productId
                }));
                trans.Commit();
            }
            return imageIds;
        }
    }
}
