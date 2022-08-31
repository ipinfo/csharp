using System.Collections.Generic;
using System.Net.Http;

using IPinfo.Http.Client;
using IPinfo.Http.Request;
using IPinfo.Exceptions;
using IPinfo.Cache;

namespace IPinfo.Apis
{
    /// <summary>
    /// The base class for all api classes.
    /// </summary>
    public class BaseApi
    {
        private const string DefaultBaseUrl = "https://ipinfo.io/";
	    
        // version is appended in the user agent header, need to update when releasing new version
        private const string DefaultUserAgent = "IPinfoClient/C#/2.0.2";

        /// <summary>
        /// HttpClient instance.
        /// </summary>
        private readonly IHttpClient _httpClient;
        
        /// <summary>
        /// CacheHandler instance.
        /// </summary>
        private readonly CacheHandler _cacheHandler;
                
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApi"/> class.
        /// </summary>
        /// <param name="httpClient">HttpClient for the API.</param>
        /// <param name="token">Token for the API.</param>
        internal BaseApi(
            IHttpClient httpClient,
            string token,
            CacheHandler cacheHandler)
        {
            this._httpClient = httpClient;
            this.Token = token;
            this._cacheHandler = cacheHandler;
        }

        /// <summary>
        /// Gets Token instance.
        /// </summary>
        internal string Token { get; }

        /// <summary>
        ///  Gets User-Agent header value.
        /// </summary>
        internal string UserAgent => DefaultUserAgent;

        /// <summary>
        ///  Gets base url value.
        /// </summary>
        internal string BaseUrl => DefaultBaseUrl;

        /// <summary>
        /// Get default HTTP client instance.
        /// </summary>
        /// <returns> IHttpClient. </returns>
        internal IHttpClient GetClientInstance()
        {
            return this._httpClient;
        }

        /// <summary>
        /// Validates the response against HTTP errors defined at the API level.
        /// </summary>
        /// <param name="context">Context of the request and the recieved response.</param>
        internal void ValidateResponse(HttpContext context)
        {
            // [429] = Request Quota Exceeded Exception
            if (context.Response.StatusCode == 429)
            {
                throw new RequestQuotaExceededException(context);
            }

            // ensure success, this should be at the bottom of all the failure checks
            if (!context.Response.OriginalResponseMessage.IsSuccessStatusCode)
            {
                // request failed
                try
                {
                    context.Response.OriginalResponseMessage.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException ex)
                {
                    throw new RequestFailedGeneralException(context, ex);
                }
            }
        }

        /// <summary>
        /// Creates Get request for given url with default header settings.
        /// </summary>
        /// <param name="queryUrl">Context of the request and the recieved response.</param>
        /// <returns> HttpRequest. </returns>
        internal HttpRequest CreateGetRequest(string queryUrl)
        {
            // append request with appropriate default headers.
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this._httpClient.Get(queryUrl, headers, this.Token);
           return httpRequest;
        }

        /// <summary>
        /// Tells if cache is enabled.
        /// </summary>
        /// <returns>True if cache is enabled.</returns>
        private bool IsCacheEnabled()
        {
            if(this._cacheHandler != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets item from cache using the CacheHandler. Returns null if cache is not available or item is not found.
        /// </summary>
        /// <param name="key"> The key against wihich cache item needs to be returned.</param>
        internal object GetFromCache(string key)
        {
            if(!IsCacheEnabled())
            {
                return null;
            }
            return this._cacheHandler.Get(key);
        }

        /// <summary>
        /// Sets item in cache using the CacheHandler if the cache is available.
        /// </summary>
        /// <param name="key"> The key against wihich item needs to be saved in the cache.</param>
        /// <param name="item"> The item that needs to be saved in the cache.</param>
        internal void SetInCache(string key, object item)
        {
            if(IsCacheEnabled())
            {
                this._cacheHandler.Set(key, item);
            }
        }
    }
}
