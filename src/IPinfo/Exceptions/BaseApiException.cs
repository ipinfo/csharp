using System;
using IPinfo.Http.Client;

namespace IPinfo.Exceptions
{
    /// <summary>
    /// This is the class for all exceptions that represent an error response from the server.
    /// </summary>
    public class BaseApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiException"/> class.
        /// </summary>
        /// <param name="reason"> The reason for throwing exception.</param>
        /// <param name="context"> The HTTP context that encapsulates request and response objects.</param>
        public BaseApiException(string reason, HttpContext context)
            : base(reason)
        {
            this.HttpContext = context;
        }

        /// <summary>
        /// Gets the HTTP response code from the API request.
        /// </summary>
        public int ResponseCode
        {
            get { return this.HttpContext.Response.StatusCode; }
        }

        /// <summary>
        /// Gets or sets the HttpContext for the request and response.
        /// </summary>
        public HttpContext HttpContext { get; internal set; }
    }
}