using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using IPinfo.Models;
using IPinfo.Utilities;
using IPinfo.Http.Request;
using IPinfo.Http.Response;

namespace IPinfo.Http.Client
{
    internal sealed class HttpClientWrapper : IHttpClient
    {
        private HttpClient client;
        private bool overrideHttpClientConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
        /// </summary>
        /// <param name="httpClientConfig"> HttpClientConfiguration object.</param>
        public HttpClientWrapper(HttpClientConfiguration httpClientConfig)
        {
            this.client = httpClientConfig.HttpClientInstance;
            this.overrideHttpClientConfiguration = httpClientConfig.OverrideHttpClientConfiguration;

            if (overrideHttpClientConfiguration)
            {
                this.client.Timeout = httpClientConfig.Timeout;
            }
        }

        /// <summary>
        /// Executes the http request.
        /// </summary>
        /// <param name="request">Http request.</param>
        /// <returns>HttpStringResponse.</returns>
        public HttpStringResponse ExecuteAsString(HttpRequest request)
        {
            Task<HttpStringResponse> t = this.ExecuteAsStringAsync(request);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Executes the http request asynchronously.
        /// </summary>
        /// <param name="request">Http request.</param>
        /// <param name="cancellationToken"> cancellationToken.</param>
        /// <returns>Returns the HttpStringResponse.</returns>
        public async Task<HttpStringResponse> ExecuteAsStringAsync(
            HttpRequest request,
            CancellationToken cancellationToken = default)
        {
            HttpResponseMessage responseMessage = await this.Execute(request, cancellationToken).ConfigureAwait(false);
            // TODO: Should EnsureSuccessStatusCode() to be called on HttpResponseMessage object?
            //responseMessage.EnsureSuccessStatusCode();

            int statusCode = (int)responseMessage.StatusCode;
            var headers = GetCombinedResponseHeaders(responseMessage);
            Stream rawBody = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            string body = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var response = new HttpStringResponse(statusCode, headers, rawBody, body);

            return response;
        }

        /// <summary>
        /// Get http request.
        /// </summary>
        /// <param name="queryUrl">queryUrl.</param>
        /// <param name="headers">headers.</param>
        /// <param name="token">token.</param>
        /// <param name="queryParameters">queryParameters.</param>
        /// <returns>HttpRequest.</returns>
        public HttpRequest Get(
            string queryUrl,
            Dictionary<string, string> headers,
            string token = null,
            Dictionary<string, object> queryParameters = null)
        {
            return new HttpRequest(HttpMethod.Get, queryUrl, headers, token, queryParameters: queryParameters);
        }

        /// <summary>
        /// Get http request.
        /// </summary>
        /// <param name="queryUrl">queryUrl.</param>
        /// <returns>HttpRequest.</returns>
        public HttpRequest Get(string queryUrl)
        {
            return new HttpRequest(HttpMethod.Get, queryUrl);
        }

        private async Task<HttpResponseMessage> Execute(
            HttpRequest request,
            CancellationToken cancellationToken)
        {
            // TODO: Currently support only plain url based Get request
            
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(request.QueryUrl),
                Method = request.HttpMethod,
            };

            if (request.Headers != null)
            {
                foreach (var headers in request.Headers)
                {
                    requestMessage.Headers.TryAddWithoutValidation(headers.Key, headers.Value);
                }
            }

            if (!string.IsNullOrEmpty(request.Token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(
                    "Bearer", request.Token);
            }

            return await this.client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
        }

        private static Dictionary<string, string> GetCombinedResponseHeaders(HttpResponseMessage responseMessage)
        {
            var headers = responseMessage.Headers.ToDictionary(l => l.Key, k => k.Value.First());
            if (responseMessage.Content != null)
            {
                foreach (var contentHeader in responseMessage.Content.Headers)
                {
                    if (headers.ContainsKey(contentHeader.Key))
                    {
                        continue;
                    }

                    headers.Add(contentHeader.Key, contentHeader.Value.First());
                }
            }

            return headers;
        }             
    }
}