namespace Derprecated.Api.Services
{
    using Handlers;
    using Models.Dto;
    using Models.Dto.Stripe;
    using ServiceStack;
    using ServiceStack.Logging;

    public class StripeService : BaseService, IPost<Event>
    {
        protected static ILog Log = LogManager.GetLogger(typeof (StripeService));

        public StripeHandler StripeHandler {get;set;}

        public object Post(Event request)
        {
            // Verify event
            var serverEvent = StripeHandler.GetEvent(request.Id);
            if(serverEvent.Id.Equals(request.Id))
            {
              // Valid

            }
            return new Dto<bool>{ Result = true };
        }
    }
}
