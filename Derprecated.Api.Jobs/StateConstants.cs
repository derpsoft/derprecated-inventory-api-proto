namespace Derprecated.Api.Jobs
{
    public static class StateConstants
    {
        /// <summary>
        ///     The last time the job ran.
        /// </summary>
        public const string JOB_LAST_RUN = "twitter.v1.job.lastRun";

        /// <summary>
        ///     The last_id received in the most recent handleQuery.
        /// </summary>
        public const string HANDLE_SEARCH_QUERY_LAST_ID = "twitter.v1.job.handleQuery.lastId";

        public const string HANDLE_SEARCH_QUERY_SINCE_ID = "twitter.v1.job.handleQuery.sinceId";
        public const string HANDLE_SEARCH_QUERY_MAX_ID = "twitter.v1.job.handleQuery.maxId";
    }
}
