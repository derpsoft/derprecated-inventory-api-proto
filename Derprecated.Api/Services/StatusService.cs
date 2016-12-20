namespace Derprecated.Api.Services
{
    using Api.Models.Routing;
    using ServiceStack.Logging;

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
