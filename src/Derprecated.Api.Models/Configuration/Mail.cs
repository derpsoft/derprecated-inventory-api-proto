namespace Derprecated.Api.Models.Configuration
{
    public sealed class Mail
    {
        public string From { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public bool UseSsl { get; set; }
    }
}
