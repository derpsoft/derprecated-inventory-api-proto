using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    [Route("/1.1/statuses/filter.json")]
    public class StatusesFilter
    {
        [Alias("track")]
        [DataMember(Name = "track")]
        public string Track { get; set; }

        [Alias("delimited")]
        [DataMember(Name = "delimited")]
        public string Delimited { get; set; }
    }
}