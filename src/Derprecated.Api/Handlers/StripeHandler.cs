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

    public StripeCharge CaptureChargeWithToken(int amountAsCents, string cardToken, string currency = "usd")
    {
      var charge = new ChargeStripeCustomer
      {
          Amount = amountAsCents,
          Currency = currency,
          Card = cardToken,
          Capture = true,
      };
      return StripeGateway.Post(charge);
    }
  }
}
