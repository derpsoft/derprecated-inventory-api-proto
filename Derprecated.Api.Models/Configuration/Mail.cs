namespace Derprecated.Api.Models.Configuration
{
    using Microsoft.Extensions.Configuration;

    public sealed class Mail
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }
}
