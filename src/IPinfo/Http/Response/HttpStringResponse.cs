using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace IPinfo.Http.Response
{
    /// <summary>
    /// HttpStringResponse inherits from HttpResponse and has additional property
    /// of string body.
    /// </summary>
    public sealed class HttpStringResponse : HttpResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpStringResponse"/> class.
        /// </summary>
        /// <param name="statusCode">statusCode.</param>
        /// <param name="headers">headers.</param>
        /// <param name="rawBody">rawBody.</param>
        /// <param name="body">body.</param>
        /// <param name="originalResponseMessage">body.</param>
        public HttpStringResponse(int statusCode, Dictionary<string, string> headers, Stream rawBody, string body, HttpResponseMessage originalResponseMessage) 
            : base(statusCode, headers, rawBody, originalResponseMessage)
        {
            this.Body = body;
        }

        /// <summary>
        /// Gets the raw string body of the http response.
        /// </summary>
        public string Body { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Body = {this.Body}" +
                $"{base.ToString()}: ";
        }

        /// <inheritdoc/>
        public override string GetResponseDetailsForException()
        {
            return $" StatusCode = {this.StatusCode},\n" +
                $"Body = {this.Body}";
        }
    }
}
