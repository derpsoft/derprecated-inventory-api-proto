namespace Derprecated.Api.Jobs.Models
{
    using System.Runtime.Serialization;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/1.1/search/tweets.json", "GET")]
    [DataContract]
    public class SearchTweets : IReturn<SearchTweetsResponse>
    {
        public SearchTweets()
        {
            Language = "en";
            ResultType = "recent";
            Count = 100;
            IncludeEntities = false;
        }

        /// <summary>
        ///     The number of tweets to return per page, up to a maximum of 100. This was formerly the "rpp" parameter in the old
        ///     Search API.
        ///     Default: 100
        /// </summary>
        [DataMember(Name = "count")]
        [Default(100)]
        public int Count { get; set; }

        /// <summary>
        ///     The entities node will be disincluded when set to false.
        ///     Default: false
        /// </summary>
        [DataMember(Name = "include_entities")]
        [Default(typeof (bool), "false")]
        public bool IncludeEntities { get; set; }

        /// <summary>
        ///     Restricts tweets to the given language, given by an ISO 639-1 code. Language detection is best-effort.
        ///     Default: en
        /// </summary>
        [DataMember(Name = "lang")]
        [Default(typeof (string), "en")]
        public string Language { get; set; }

        /// <summary>
        ///     Returns results with an ID less than (that is, older than) or equal to the specified ID.
        /// </summary>
        [DataMember(Name = "max_id")]
        public long MaxId { get; set; }

        /// <summary>
        ///     A UTF-8, URL-encoded search query of 500 characters maximum, including operators. Queries may additionally be
        ///     limited by complexity.
        /// </summary>
        [DataMember(Name = "q")]
        [Required]
        public string Query { get; set; }

        /// <summary>
        ///     Optional. Specifies what type of search results you would prefer to receive. The current default is "mixed." Valid
        ///     values include:
        ///     * mixed: Include both popular and real time results in the response.
        ///     * recent: return only the most recent results in the response
        ///     * popular: return only the most popular results in the response.
        ///     Default: recent
        /// </summary>
        [DataMember(Name = "result_type")]
        [Default(typeof (string), "recent")]
        public string ResultType { get; set; }

        /// <summary>
        ///     Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the
        ///     number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the
        ///     since_id will be forced to the oldest ID available.
        /// </summary>
        [DataMember(Name = "since_id")]
        public long SinceId { get; set; }

        /// <summary>
        ///     Conditional serializing method to skip params that are not set, like since_id and max_id
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool? ShouldSerialize(string fieldName)
        {
            switch (fieldName)
            {
                case "MaxId":
                    return MaxId > 0;
                case "SinceId":
                    return SinceId > 0;
                default:
                    return true;
            }
        }
    }
}
