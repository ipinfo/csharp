using IPinfo.Http.Client;

namespace IPinfo.Exceptions
{
    /// <summary>
    /// This is the class for RequestQuotaExceededException that represent an error code 429 from the server.
    /// </summary>
    public class RequestQuotaExceededException : BaseApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestQuotaExceededException"/> class.
        /// </summary>
        /// <param name="context"> The HTTP context that encapsulates request and response objects.</param>
        public RequestQuotaExceededException(HttpContext context)
            : base("You have been sending too many requests. Visit https://ipinfo.io/account to see your API limits.", context)
        {
            
        }
    }
}