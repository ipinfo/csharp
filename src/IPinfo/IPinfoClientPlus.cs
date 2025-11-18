using System;

using IPinfo.Http.Client;
using IPinfo.Apis;
using IPinfo.Cache;
using IPinfo.Utilities;

namespace IPinfo
{
    /// <summary>
    /// The gateway for IPinfo Plus SDK. This class holds the configuration of the SDK.
    /// </summary>
    public sealed class IPinfoClientPlus
    {
        private readonly IHttpClient _httpClient;
        private readonly CacheHandler _cacheHandler;
        private readonly Lazy<IPApiPlus> _ipApi;

        private IPinfoClientPlus(
            string accessToken,
            IHttpClient httpClient,
            CacheHandler cacheHandler,
            IHttpClientConfiguration httpClientConfiguration)
        {
            this._httpClient = httpClient;
            this._cacheHandler = cacheHandler;
            this.HttpClientConfiguration = httpClientConfiguration;

            this._ipApi = new Lazy<IPApiPlus>(
                () => new IPApiPlus(this._httpClient, accessToken, cacheHandler));
        }

        /// <summary>
        /// Gets IPApiPlus.
        /// </summary>
        public IPApiPlus IPApi => this._ipApi.Value;

        /// <summary>
        /// Gets the configuration of the Http Client associated with this client.
        /// </summary>
        public IHttpClientConfiguration HttpClientConfiguration { get; }

        /// <summary>
        /// Gets the configuration of the Http Client associated with this client.
        /// </summary>
        public ICache Cache { get => _cacheHandler?.Cache; }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private string _accessToken = "";
            private HttpClientConfiguration.Builder _httpClientConfig = new HttpClientConfiguration.Builder();
            private IHttpClient _httpClient;
            private CacheHandler _cacheHandler = new CacheHandler();

            /// <summary>
            /// Sets credentials for BearerAuth.
            /// </summary>
            /// <param name="accessToken">AccessToken.</param>
            /// <returns>Builder.</returns>
            public Builder AccessToken(string accessToken)
            {
                this._accessToken = accessToken;
                return this;
            }

            /// <summary>
            /// Sets HttpClientConfig.
            /// </summary>
            /// <param name="action"> Action. </param>
            /// <returns>Builder.</returns>
            public Builder HttpClientConfig(Action<HttpClientConfiguration.Builder> action)
            {
                if (action is null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                action(this._httpClientConfig);
                return this;
            }

            /// <summary>
            /// Sets the ICache implementation for the Builder.
            /// </summary>
            /// <param name="cache"> ICache implementation. Pass null to disable the cache.</param>
            /// <returns>Builder.</returns>
            public Builder Cache(ICache cache)
            {
                // Null is allowed here, which is being used to indicate that user do not want the cache.
                if(cache == null)
                {
                    this._cacheHandler = null;
                }
                else
                {
                    this._cacheHandler = new CacheHandler(cache);
                }
                return this;
            }

            /// <summary>
            /// Creates an object of the IPinfoClientPlus using the values provided for the builder.
            /// </summary>
            /// <returns>IPinfoClientPlus.</returns>
            public IPinfoClientPlus Build()
            {
                this._httpClient = new HttpClientWrapper(this._httpClientConfig.Build());

                return new IPinfoClientPlus(
                    _accessToken,
                    _httpClient,
                    _cacheHandler,
                    _httpClientConfig.Build());
            }
        }
    }
}
