namespace Derprecated.Api.Handlers
{
  using System;
  using System.Collections.Generic;
  using Models;
  using ServiceStack;
  using ServiceStack.Data;
  using ServiceStack.OrmLite;
  using ServiceStack.Stripe;
  using ServiceStack.Stripe.Types;

  public class StripeHandler
  {
    public StripeHandler(string secretKey)
    {
      StripeGateway = new StripeGateway(secretKey);
    }

    private StripeGateway StripeGateway {get;set;}

    public object CreateCustomerWithToken(string email, string token)
    {
      var customer = new CreateStripeCustomerWithToken {
        Email = email,
        Card = token,
      };
      throw new NotImplementedException();
    }

    public StripeCharge CaptureChargeWithToken(int amountAsCents, string orderNumber, string cardToken, string description = "", string currency = "usd")
    {
      var charge = new ChargeStripeCustomerWithMetadata
      {
          Amount = amountAsCents,
          Currency = currency,
          Card = cardToken,
          Capture = true,
          Description = description,
          Metadata = {
            {"order_number", orderNumber}
          },
      };
      return StripeGateway.Post(charge);
    }
  }

  [Route("/charges")]
  class ChargeStripeCustomerWithMetadata : ChargeStripeCustomer
  {
      public ChargeStripeCustomerWithMetadata()
      {
          Metadata = new Dictionary<string,string>();
      }

      public Dictionary<string,string> Metadata { get; set; }
  }
}
