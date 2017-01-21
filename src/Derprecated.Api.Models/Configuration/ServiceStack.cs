namespace Derprecated.Api.Models.Configuration
{
    using Microsoft.Extensions.Configuration;

    public sealed class ServiceStack
    {
        public string License { get; set; }

        public static ServiceStack From(IConfigurationSection config)
        {
            return new ServiceStack
            {
                License = config["license"]
            };
        }
    }
}
