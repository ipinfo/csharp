using System;
using System.Collections.Generic;
using System.IO;
using IPinfo.Utilities;
using IPinfo.Http.Client;
using IPinfo.Http.Response;
using IPinfo.Exceptions;

// TODO: Need to be viewed/improved to get completed.
namespace IPinfo.Apis
{
    /// <summary>
    /// The base class for all api classes.
    /// </summary>
    public class BaseApi
    {
        private const string defaultBaseURL   = "https://ipinfo.io/";
	    private const string defaultUserAgent = "IPinfoClient/C#/2.6.0";

        /// <summary>
        /// HttpClient instance.
        /// </summary>
        private readonly IHttpClient httpClient;
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApi"/> class.
        /// </summary>
        /// <param name="httpClient">HttpClient for the API.</param>
        /// <param name="token">Token for the API.</param>
        internal BaseApi(
            IHttpClient httpClient,
            string token)
        {
            this.httpClient = httpClient;
            this.Token = token;
        }

        /// <summary>
        /// Gets Token instance.
        /// </summary>
        internal string Token { get; }

        /// <summary>
        ///  Gets User-Agent header value.
        /// </summary>
        protected string UserAgent => defaultUserAgent;

        /// <summary>
        ///  Gets base url value.
        /// </summary>
        protected string BaseUrl => defaultBaseURL;

        /// <summary>
        /// Gets HttpClientWrapper instance.
        /// </summary>
        internal HttpClientWrapper HttpClientWrapper { get; }

        /// <summary>
        /// Get default HTTP client instance.
        /// </summary>
        /// <returns> IHttpClient. </returns>
        internal IHttpClient GetClientInstance()
        {
            return this.httpClient;
        }

        /// <summary>
        /// Validates the response against HTTP errors defined at the API level.
        /// </summary>
        /// <param name="response">The response recieved.</param>
        /// <param name="context">Context of the request and the recieved response.</param>
        protected void ValidateResponse(HttpResponse response, HttpContext context)
        {
            // [429] = Rate limited exception
            if (response.StatusCode == 429)
            {
                throw new RateLimitedException(context);
            }            
        }
    }
}