using BausCode.Api.Models.Routing;
using ServiceStack;
using ServiceStack.Logging;

namespace BausCode.Api.Services
{
    public class StatusService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (StatusService));

        public object Any(GetQueueStatus request)
        {
            var response = new GetQueueStatusResponse();

            //response.ProcessingQueueLength = Redis.GetListCount(Configuration.QueueName);

            return response;
        }
    }
}