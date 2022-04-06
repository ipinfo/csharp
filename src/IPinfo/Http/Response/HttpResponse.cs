using System.Collections.Generic;
using System.IO;
using System.Net.Http;

using IPinfo.Utilities;

namespace IPinfo.Http.Response
{
    /// <summary>
    /// HttpResponse stores necessary information about the http response.
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponse"/> class.
        /// </summary>
        /// <param name="statusCode">statusCode.</param>
        /// <param name="headers">headers.</param>
        /// <param name="rawBody">rawBody.</param>
        /// <param name="originalResponseMessage">rawBody.</param>
        public HttpResponse(int statusCode, Dictionary<string, string> headers, Stream rawBody, HttpResponseMessage originalResponseMessage)
        {
            this.StatusCode = statusCode;
            this.Headers = headers;
            this.RawBody = rawBody;
            this.OriginalResponseMessage = originalResponseMessage;
        }

        /// <summary>
        /// Gets the HTTP Status code of the http response.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Gets the headers of the http response.
        /// </summary>
        public Dictionary<string, string> Headers { get; }

        /// <summary>
        /// Gets the stream of the body.
        /// </summary>
        public Stream RawBody { get; }

        /// <summary>
        /// Gets the original response message.
        /// </summary>
        public HttpResponseMessage OriginalResponseMessage { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $" StatusCode = {this.StatusCode}, " +
                $" Headers = {JsonHelper.Serialize(this.Headers)}, " +
                $" RawBody = {this.RawBody}";
        }
    }
}
