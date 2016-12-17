namespace Derprecated.Api.Jobs.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Location
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "woeid")]
        // ReSharper disable once InconsistentNaming
        public int WOEID { get; set; }
    }
}
