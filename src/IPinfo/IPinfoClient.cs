using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

using IPinfo.Http.Client;
using IPinfo.Apis;
using IPinfo.Cache;
using IPinfo.Utilities;

namespace IPinfo
{
    public sealed class IPinfoClient
    {
        private readonly IDictionary<string, List<string>> additionalHeaders;
        private readonly IHttpClient httpClient;
        private readonly CacheHandler cacheHandler;
        private readonly Lazy<IPApi> ipApi;

        private IPinfoClient(
            string accessToken,
            IHttpClient httpClient,
            CacheHandler cacheHandler,
            IDictionary<string, List<string>> additionalHeaders = null,
            IHttpClientConfiguration httpClientConfiguration = null)
        {
            this.httpClient = httpClient;
            this.cacheHandler = cacheHandler;
            this.additionalHeaders = additionalHeaders;
            this.HttpClientConfiguration = httpClientConfiguration;
            
            this.ipApi = new Lazy<IPApi>(
                () => new IPApi(this.httpClient, accessToken, cacheHandler));

            CountryHelper.Init();
        }

        /// <summary>
        /// Gets IPApi.
        /// </summary>
        public IPApi IPApi => this.ipApi.Value;
        
        /// <summary>
        /// Gets the configuration of the Http Client associated with this client.
        /// </summary>
        public IHttpClientConfiguration HttpClientConfiguration { get; }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private string accessToken = "";
            private HttpClientConfiguration.Builder httpClientConfig = new HttpClientConfiguration.Builder();
            private IHttpClient httpClient;
            private CacheHandler cacheHandler = new CacheHandler();
            private IDictionary<string, List<string>> additionalHeaders = new Dictionary<string, List<string>>();

            /// <summary>
            /// Sets credentials for BearerAuth.
            /// </summary>
            /// <param name="accessToken">AccessToken.</param>
            /// <returns>Builder.</returns>
            public Builder AccessToken(string accessToken)
            {
                this.accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
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

                action(this.httpClientConfig);
                return this;
            }

            /// <summary>
            /// Sets the AdditionalHeaders for the Builder.
            /// </summary>
            /// <param name="additionalHeaders"> additional headers. </param>
            /// <returns>Builder.</returns>
            public Builder AdditionalHeaders(IDictionary<string, List<string>> additionalHeaders)
            {
                if (additionalHeaders is null)
                {
                    throw new ArgumentNullException(nameof(additionalHeaders));
                }

                this.additionalHeaders = additionalHeaders.ToDictionary(s => s.Key, s => new List<string>(s.Value));
                return this;
            }

            /// <summary>
            /// Adds AdditionalHeader.
            /// </summary>
            /// <param name="headerName"> header name. </param>
            /// <param name="headerValue"> header value. </param>
            /// <returns>Builder.</returns>
            public Builder AddAdditionalHeader(string headerName, string headerValue)
            {
                if (string.IsNullOrWhiteSpace(headerName))
                {
                    throw new ArgumentException("Header name can not be null, empty or whitespace.", nameof(headerName));
                }

                if (headerValue is null)
                {
                    throw new ArgumentNullException(nameof(headerValue));
                }

                if (this.additionalHeaders.ContainsKey(headerName) && this.additionalHeaders[headerName] != null)
                {
                    this.additionalHeaders[headerName].Add(headerValue);
                }
                else
                {
                    this.additionalHeaders[headerName] = new List<string>() { headerValue };
                }

                return this;
            }

            /// <summary>
            /// Sets the ICache for the Builder.
            /// </summary>
            /// <param name="cache"> ICache implementation. Pass null to disable the cache.</param>
            /// <returns>Builder.</returns>
            public Builder Cache(ICache cache)
            {
                // Null is allowed here, which is being used to indicate that user do not want the cache.
                if(cache == null)
                {
                    this.cacheHandler = null;
                }
                else
                {
                    this.cacheHandler = new CacheHandler(cache);
                }
                return this;
            }

            /// <summary>
            /// Creates an object of the IPinfoClient using the values provided for the builder.
            /// </summary>
            /// <returns>IPinfoClient.</returns>
            public IPinfoClient Build()
            {
                this.httpClient = new HttpClientWrapper(this.httpClientConfig.Build());

                return new IPinfoClient(
                    this.accessToken,
                    this.httpClient,
                    this.cacheHandler,
                    this.additionalHeaders,
                    this.httpClientConfig.Build());
            }
        }
    }
}