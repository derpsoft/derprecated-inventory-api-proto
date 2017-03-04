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
    }
}
