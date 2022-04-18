using System.Net.Http;

using IPinfo.Http.Client;

namespace IPinfo.Exceptions
{
    /// <summary>
    /// This is the class for RequestFailedGeneralException that represent general error on the api request.
    /// </summary>
    public class RequestFailedGeneralException : BaseApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestFailedGeneralException"/> class.
        /// </summary>
        /// <param name="context"> The HTTP context that encapsulates request and response objects.</param>
        /// <param name="httpRequestException"> The inner HttpRequestException.</param>
        public RequestFailedGeneralException(HttpContext context, HttpRequestException httpRequestException)
            : base(context.Response.GetResponseDetailsForException(), context, httpRequestException)
        {
        }
    }
}
