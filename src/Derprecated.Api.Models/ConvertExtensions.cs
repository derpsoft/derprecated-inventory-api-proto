namespace Derprecated.Api.Models
{
    using Dto;
    using ServiceStack;

    public static class ConvertExtensions
    {
        // See http://docs.servicestack.net/auto-mapping#advanced-mapping-using-custom-extension-methods
        // for details on how to use this class

        public static User ToDto(this Auth0.Core.User from)
        {
            var to = from.ConvertTo<User>();
            var metadata = from.AppMetadata.ToObject<Dto.Auth0.AppMetadata>();

            to.Id = from.UserId;
            to.Permissions = metadata.Authorization.Permissions;

            return to;
        }

        public static Auth0.Core.User FromDto(this User from)
        {
          var to = from.ConvertTo<Auth0.Core.User>();

          to.UserId = from.Id;

          return to;
        }

        public static Order FromDto(this Dto.Order from)
        {
          var to = from.ConvertTo<Order>();
          to.ShippingCustomer = from.ShippingCustomer.ConvertTo<Customer>();
          to.BillingCustomer = from.BillingCustomer.ConvertTo<Customer>();
          to.AcceptedOffers = from.AcceptedOffers.ConvertAll(x => x.ConvertTo<Offer>());
          return to;
        }

        public static Dto.Order ToDto(this Order from) {
          var to = from.ConvertTo<Dto.Order>();
          to.ShippingCustomer = from.ShippingCustomer.ConvertTo<Dto.Customer>();
          to.BillingCustomer = from.BillingCustomer.ConvertTo<Dto.Customer>();
          to.AcceptedOffers = from.AcceptedOffers.ConvertAll(x => x.ConvertTo<Dto.Offer>());
          return to;
        }
    }
}
