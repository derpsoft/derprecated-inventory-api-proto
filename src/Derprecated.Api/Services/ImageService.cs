namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Handlers;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using Image = Models.Image;
    using Product = Models.Dto.Product;

    public class ImageService : CrudService<Image, Models.Dto.Image>
    {
        public ImageService(IHandler<Image> handler, IHandler<ProductImage> productImageHandler) : base(handler)
        {
            ProductImageHandler = (ProductImageHandler) productImageHandler;
        }

        private ImageHandler ImageHandler => Handler as ImageHandler;

        private ProductImageHandler ProductImageHandler { get; }

        protected override object Create(Models.Dto.Image request)
        {
            var resp = new Dto<Models.Dto.Image>();
            if (Request.Files.Length > 0)
            {
                var image = request.ConvertTo<Image>();
                image.Url = ImageHandler.SaveImage(Request.Files.First(), "images").ToString();
                resp.Result = Handler.Save(image).ConvertTo<Models.Dto.Image>();
            }
            return resp;
        }

        protected override object Update(Models.Dto.Image request)
        {
            var dto = (Dto<Models.Dto.Image>) base.Update(request);

            dto.Result.ProductIds = ProductImageHandler.AssociateProducts(dto.Result.Id, request.ProductIds);

            return dto;
        }

        public object Get(ImageCount request)
        {
            var resp = new Dto<long>();

            resp.Result = Handler.Count(request.IncludeDeleted);

            return resp;
        }

        public object Get(Images request)
        {
            var resp = new Dto<List<Models.Dto.Image>>();

            resp.Result =
                Handler.List(request.Skip, request.Take, request.IncludeDeleted)
                       .ConvertAll(x => x.ConvertTo<Models.Dto.Image>());

            return resp;
        }

        public object Any(ImageTypeahead request)
        {
            var resp = new Dto<List<Models.Dto.Image>>();

            if (request.Query.IsNullOrEmpty())
                resp.Result =
                    Handler.List(0, int.MaxValue, request.IncludeDeleted)
                           .ConvertAll(x => x.ConvertTo<Models.Dto.Image>());

            else
                resp.Result =
                    Handler.Typeahead(request.Query, request.IncludeDeleted)
                           .ConvertAll(x => x.ConvertTo<Models.Dto.Image>());


            return resp;
        }

        public override object Get(Models.Dto.Image request)
        {
            var resp = (Dto<Models.Dto.Image>) base.Get(request);

            resp.Result.Products =
                ProductImageHandler.GetProducts(resp.Result.Id, request.IncludeDeleted)
                                   .ConvertAll(x => x.ConvertTo<Product>());

            return resp;
        }
    }
}
