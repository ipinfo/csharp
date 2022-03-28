using IPinfo.Http.Client;
using IPinfo.Exceptions;
using IPinfo.Cache;

// TODO: Need to be viewed/improved to get completed.
namespace IPinfo.Apis
{
    /// <summary>
    /// The base class for all api classes.
    /// </summary>
    public class BaseApi
    {
        private const string DefaultBaseUrl   = "https://ipinfo.io/";
	    private const string DefaultUserAgent = "IPinfoClient/C#/2.0.0";

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
        }

        /// <summary>
        /// Tells if cache is enabled.
        /// </summary>
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