namespace Derprecated.Api.Models.Dto.Stripe
{
    using Dto;
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/v1/events/{Id}", "GET")]
    public class Event : IReturn<Event>
    {
      public string Id {get;set;}

      public string Object {get;set;}
      public string ApiVersion {get;set;}
      public string Type {get;set;}
      public bool LiveMode {get;set;}
      public int PendingWebhooks {get;set;}
      public string Request {get;set;}
      public EventData Data {get;set;}
    }

    public class EventData
    {
      public Dictionary<string,string> Object {get;set;}
    }
}
