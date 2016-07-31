namespace BausCode.Api.Models
{
    public class Configuration
    {
        public string QueueName { get; set; }
        public string RestBaseUri { get; set; }
        public string StreamBaseUri { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public int BatchSize { get; set; }
        public string UserAccessTokenSecret { get; set; }
        public string UserAccessToken { get; set; }
        public string Query { get; set; }
        public string ShutdownFile { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public int KeywordReloadMinutes { get; set; }
    }
}