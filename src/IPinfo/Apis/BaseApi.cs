using IPinfo.Http.Client;
using IPinfo.Http.Response;
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
        private const string DEFAULT_BASE_URL   = "https://ipinfo.io/";
	    private const string DEFAULT_USER_AGENT = "IPinfoClient/C#/2.0.0";

        /// <summary>
        /// HttpClient instance.
        /// </summary>
        private readonly IHttpClient httpClient;
        protected CacheHandler cacheHandler;
                
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
            this.httpClient = httpClient;
            this.Token = token;
            this.cacheHandler = cacheHandler;
        }

        /// <summary>
        /// Gets Token instance.
        /// </summary>
        internal string Token { get; }

        /// <summary>
        ///  Gets User-Agent header value.
        /// </summary>
        protected string UserAgent => DEFAULT_USER_AGENT;

        /// <summary>
        ///  Gets base url value.
        /// </summary>
        protected string BaseUrl => DEFAULT_BASE_URL;

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
        protected void ValidateResponse(HttpContext context)
        {
            // [429] = Request Quota Exceeded Exception
            if (context.Response.StatusCode == 429)
            {
                throw new RequestQuotaExceededException(context);
            }
        }
    }
}