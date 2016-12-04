namespace BausCode.Api.Jobs.Models
{
    using System.Runtime.Serialization;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [DataContract]
    [Route("/1.1/statuses/filter.json")]
    public class StatusesFilter
    {
        [Alias("delimited")]
        [DataMember(Name = "delimited")]
        public string Delimited { get; set; }

        [Alias("track")]
        [DataMember(Name = "track")]
        public string Track { get; set; }
    }
}
