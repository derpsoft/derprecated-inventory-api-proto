using BausCode.Api.Handlers;
using BausCode.Api.Models.Routing;

namespace BausCode.Api.Services
{
    public class ProductService : BaseService
    {
        public GetProductResponse Any(GetProduct request)
        {
            var resp = new GetProductResponse();
            var handler = new ProductHandler(Db, CurrentUser);

            resp.Product = handler.GetProduct(request.Id);

            return resp;
        }

        public GetProductQuantityOnHandResponse Any(GetProductQuantityOnHand request)
        {
            var resp = new GetProductQuantityOnHandResponse();
            var handler = new ProductHandler(Db, CurrentUser);

            resp.Quantity = handler.GetQuantityOnHand(request.Id);

            return resp;
        }
    }
}