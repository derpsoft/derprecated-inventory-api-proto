namespace Derprecated.Api.Models.Dto.Stripe
{
    using Dto;
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/hooks/stripe")]
    public class Webhook : IReturn<Dto<Webhook>>
    {

    }
}
