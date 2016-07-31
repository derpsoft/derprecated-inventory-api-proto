using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Jobs.Models
{
    [Route("/1.1/trends/place.json", "GET")]
    [DataContract]
    public class GetTrends : IReturn<List<GetTrendsResponse>>
    {
        public GetTrends()
        {
            Id = 1;
        }

        /// <summary>
        ///     The Where-on-Earth ID (WOEID) of the place to get trending data for. Defaults to 1 (Global).
        /// </summary>
        [DataMember(Name = "id")]
        [Required]
        public int Id { get; set; }

        /// <summary>
        ///     Set to "hashtags" will remove all hashtags from the trending list.
        /// </summary>
        [DataMember(Name = "exclude")]
        public string Exclude { get; set; }

        /// <summary>
        ///     Set the Exclude property to the correct value based on whether on not the
        ///     <para>exclude</para>
        ///     param is set.
        /// </summary>
        /// <param name="exclude"></param>
        public void ExcludeHashtags(bool exclude = true)
        {
            Exclude = exclude ? "exclude" : string.Empty;
        }
    }
}