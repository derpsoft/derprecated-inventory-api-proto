namespace BausCode.Api.Jobs.Authentication
{
    using System.Net;

    /// <summary>
    ///     Not yet threadsafe
    /// </summary>
    /// <remarks>
    ///     TODO(jcunningham) Thread-safe singleton.
    /// </remarks>
    public abstract class AuthBase
    {
        public abstract bool IsAuthenticated();
        public abstract string GetToken();

        /// <summary>
        ///     Sign a service client for making requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dto"></param>
        public abstract void SignRequest(HttpWebRequest request, object dto);
    }
}
