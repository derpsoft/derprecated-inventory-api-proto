namespace Derprecated.Api.Models
{
    using Dto;
    using ServiceStack;
    using System.Linq;

    public static class ConvertExtensions
    {
        // See http://docs.servicestack.net/auto-mapping#advanced-mapping-using-custom-extension-methods
        // for details on how to use this class

        public static Dto.InventoryTransaction ToDto(this InventoryTransaction from)
        {
          var to = from.ConvertTo<Dto.InventoryTransaction>();
          to.Product = from.Product.ConvertTo<Dto.Product>();
          return to;
        }

        public static InventoryTransaction FromDto(this Dto.InventoryTransaction from)
        {
          var to = from.ConvertTo<InventoryTransaction>();
          return to;
        }

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

        public static Dto.Offer ToDto(this Offer from)
        {
          var to = from.ConvertTo<Dto.Offer>();
          to.Product = from.Product.ConvertTo<Dto.Product>();
          return to;
        }

        public static Offer FromDto(this Dto.Offer from)
        {
          var to = from.ConvertTo<Offer>();
          // to.Product = from.Product.ConvertTo<Product>();
          return to;
        }

        public static Order FromDto(this Dto.Order from)
        {
          var to = from.ConvertTo<Order>();
          to.ShippingCustomer = from.ShippingCustomer.ConvertTo<Customer>();
          to.BillingCustomer = from.BillingCustomer.ConvertTo<Customer>();
          to.Offers = from.Offers.ConvertAll(x => x.FromDto());
          return to;
        }

        public static Dto.Order ToDto(this Order from) {
          var to = from.ConvertTo<Dto.Order>();
          to.ShippingCustomer = from.ShippingCustomer.ConvertTo<Dto.Customer>();
          to.BillingCustomer = from.BillingCustomer.ConvertTo<Dto.Customer>();
          to.Offers = from.Offers.ConvertAll(x => x.ToDto());
          return to;
        }

        public static Dto.AddressSummary ToSummary(this Address from) {
          var to = from.ConvertTo<Dto.AddressSummary>();
          return to;
        }

        public static Dto.OfferSummary ToSummary(this Offer from) {
          var to = from.ConvertTo<Dto.OfferSummary>();
          return to;
        }

        public static Dto.OrderSummary ToSummary(this Order from) {
          var to = from.ConvertTo<Dto.OrderSummary>();
          to.ShippingAddress = from.ShippingAddress.ToSummary();
          to.BillingAddress = from.BillingAddress.ToSummary();
          to.Offers = from.Offers.ConvertAll(x => x.ToSummary());
          return to;
        }
    }
}
