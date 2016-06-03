using BausCode.Api.Attributes;
using BausCode.Api.Models.Routing;

namespace BausCode.Api.Services
{
    public class StatusService : BaseService
    {
        [ApiKeyAuthorize]
        public object Any(GetQueueStatus request)
        {
            var response = new GetQueueStatusResponse();

            response.ProcessingQueueLength = Redis.GetListCount(Configuration.QueueName);

            return response;
        }
    }
}