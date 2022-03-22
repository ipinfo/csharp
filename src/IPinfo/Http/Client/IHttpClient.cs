using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

using IPinfo.Http.Request;
using IPinfo.Http.Response;

namespace IPinfo.Http.Client
{
    /// <summary>
    /// IHttpClient.
    /// </summary>
    internal interface IHttpClient
    {
        /// <summary>
        /// Execute a given HttpRequest to get string response back.
        /// </summary>
        /// <param name="request">The given HttpRequest to execute.</param>
        /// <returns> HttpResponse containing raw information.</returns>
        HttpStringResponse ExecuteAsString(HttpRequest request);

        /// <summary>
        /// Execute a given HttpRequest to get async string response back.
        /// </summary>
        /// <param name="request">The given HttpRequest to execute.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns> HttpResponse containing raw information.</returns>
        Task<HttpStringResponse> ExecuteAsStringAsync(HttpRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a simple HTTP GET request given the URL.
        /// </summary>
        /// <param name="queryUrl">Url the request should be sent to.</param>
        /// <returns>HttpRequest initialised with the url specified.</returns>
        HttpRequest Get(string queryUrl);

        /// <summary>
        /// Create a simple HTTP GET request given relavent parameters.
        /// </summary>
        /// <param name="queryUrl">Url the request should be sent to.</param>
        /// <param name="headers">HTTP headers that should be included.</param>
        /// <param name="token">Auth token.</param>
        /// <param name="queryParameters">Query parameters to be included.</param>
        /// <returns> HttpRequest initialised with the http parameters specified.</returns>
        HttpRequest Get(
            string queryUrl,
            Dictionary<string, string> headers,
            string token = null,
            Dictionary<string, object> queryParameters = null);
    }
}
