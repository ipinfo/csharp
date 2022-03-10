using System;
using System.Collections.Generic;
using System.IO;
using IPinfo.Utilities;

namespace IPinfo.Http.Request
{
    /// <summary>
    /// HttpRequest stores necessary information about the http request.
    /// </summary>
    public sealed class HttpRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequest"/> class.
        /// </summary>
        /// <param name="method">Http verb to use for the http request.</param>
        /// <param name="queryUrl">The query url for the http request.</param>
        /// <param name="headers">Headers to send with the request.</param>
        /// <param name="token">Auth token.</param>
        /// <param name="queryParameters">QueryParameters.</param>
        public HttpRequest(
            HttpMethod method,
            string queryUrl,
            Dictionary<string, string> headers,
            string token,
            Dictionary<string, object> queryParameters = null)
        {
            this.HttpMethod = method;
            this.QueryUrl = queryUrl;
            this.QueryParameters = queryParameters;
            this.Headers = headers;
            this.Token = token;
        }

        /// <summary>
        /// Gets the HTTP verb to use for this request.
        /// </summary>
        public HttpMethod HttpMethod { get; }

        /// <summary>
        /// Gets the query url for the http request.
        /// </summary>
        public string QueryUrl { get; }

        /// <summary>
        /// Gets the query parameters collection for the current http request.
        /// </summary>
        public Dictionary<string, object> QueryParameters { get; private set; }

        /// <summary>
        /// Gets the headers collection for the current http request.
        /// </summary>
        public Dictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// Gets the token for Auth.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Concatenate values from a Dictionary to this object.
        /// </summary>
        /// <param name="headersToAdd"> headersToAdd. </param>
        /// <returns>Dictionary.</returns>
        public Dictionary<string, string> AddHeaders(Dictionary<string, string> headersToAdd)
        {
            if (this.Headers == null)
            {
                this.Headers = new Dictionary<string, string>(headersToAdd);
            }
            else
            {
                this.Headers = this.Headers.Concat(headersToAdd).ToDictionary(x => x.Key, x => x.Value);
            }

            return this.Headers;
        }

        /// <summary>
        /// Concatenate values from a Dictionary to query parameters dictionary.
        /// </summary>
        /// <param name="queryParamaters"> queryParamaters. </param>
        public void AddQueryParameters(Dictionary<string, object> queryParamaters)
        {
            if (this.QueryParameters == null)
            {
                this.QueryParameters = new Dictionary<string, object>(queryParamaters);
            }
            else
            {
                this.QueryParameters = this.QueryParameters.Concat(queryParamaters).ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $" HttpMethod = {this.HttpMethod}, " +
                $" QueryUrl = {this.QueryUrl}, " +
                $" QueryParameters = {JsonHelper.Serialize(this.QueryParameters)}, " +
                $" Headers = {JsonHelper.Serialize(this.Headers)}, " +
                $" Token = {this.Token}";
        }
    }
}