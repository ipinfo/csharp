using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

using IPinfo.Http.Client;
using IPinfo.Apis;

namespace IPinfo
{

    public class IPinfoClient
    {



        private readonly IDictionary<string, List<string>> additionalHeaders;
        private readonly IHttpClient httpClient;
        
        private readonly Lazy<IPApi> ipApi;

        private IPinfoClient(
            string ipinfoVersion,
            string accessToken,
            IHttpClient httpClient,
            IDictionary<string, List<string>> additionalHeaders)
        {
            this.IPinfoVersion = ipinfoVersion;
            this.httpClient = httpClient;
            this.additionalHeaders = additionalHeaders;
            
            this.ipApi = new Lazy<IPApi>(
                () => new IPApi(this.httpClient, accessToken));
            
        }

        /// <summary>
        /// Gets IPApi.
        /// </summary>
        public IPApi IPApi => this.ipApi.Value;
        
        /// <summary>
        /// Gets IPinfoVersion.
        /// IPinfo API versions.
        /// </summary>
        public string IPinfoVersion { get; }

        

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private string accessToken = "";
            private string ipinfoVersion = "";
            private IHttpClient httpClient;
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
            /// Sets the IHttpClient for the Builder.
            /// </summary>
            /// <param name="httpClient"> http client. </param>
            /// <returns>Builder.</returns>
            internal Builder HttpClient(IHttpClient httpClient)
            {
                this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
                return this;
            }

            /// <summary>
            /// Creates an object of the IPinfoClient using the values provided for the builder.
            /// </summary>
            /// <returns>IPinfoClient.</returns>
            public IPinfoClient Build(HttpClient httpClient)
            {
                this.httpClient = new HttpClientWrapper(httpClient);

                return new IPinfoClient(
                    this.ipinfoVersion,//pass constant value from here
                    this.accessToken,
                    this.httpClient,
                    this.additionalHeaders);
            }
        }
    }
}
