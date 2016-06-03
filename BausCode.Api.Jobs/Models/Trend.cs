using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class Trend
    {

        [DataMember(Name = "tweet_volume")]
        public int TweetVolume { get; set; }

        /// <summary>
        /// Not sure what this does, the Twitter documentation does not say.
        /// </summary>
        [DataMember(Name = "events")]
        public object Events { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Not sure what this does, the Twitter documentation does not say.
        /// </summary>
        [DataMember(Name = "promoted_context")]
        public object PromotedContext { get; set; }

        [DataMember(Name = "query")]
        public string Query { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
