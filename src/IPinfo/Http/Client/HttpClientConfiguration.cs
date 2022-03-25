using System;
using System.Net.Http;

namespace IPinfo.Http.Client
{
    /// <summary>
    /// HttpClientConfiguration represents the current state of the Http Client.
    /// </summary>
    public class HttpClientConfiguration : IHttpClientConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientConfiguration"/>
        /// class.
        /// </summary>
        private HttpClientConfiguration(
            TimeSpan timeout,
            HttpClient httpClientInstance,
            bool overrideHttpClientConfiguration)
        {
            this.Timeout = timeout;
            this.HttpClientInstance = httpClientInstance;
            this.OverrideHttpClientConfiguration = overrideHttpClientConfiguration;
        }

        /// <summary>
        /// Gets Http client timeout.
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Gets HttpClient instance used to make the HTTP calls
        /// </summary>
        public HttpClient HttpClientInstance { get; }

        /// <summary>
        /// Gets Boolean which allows the SDK to override http client instance's settings used for features like retries, timeouts etc.
        /// </summary>
        public bool OverrideHttpClientConfiguration { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "HttpClientConfiguration: " +
                $"{this.Timeout} , " +
                $"{this.HttpClientInstance} , " +
                $"{this.OverrideHttpClientConfiguration} ";
        }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private TimeSpan timeout = TimeSpan.FromSeconds(60);
            private HttpClient httpClientInstance = new HttpClient();
            private bool overrideHttpClientConfiguration = true;

            /// <summary>
            /// Sets the Timeout.
            /// </summary>
            /// <param name="timeout"> Timeout. </param>
            /// <returns>Builder.</returns>
            public Builder Timeout(TimeSpan timeout)
            {
                this.timeout = timeout.TotalSeconds <= 0 ? TimeSpan.FromSeconds(60) : timeout;
                return this;
            }

            /// <summary>
            /// Sets the HttpClientInstance.
            /// </summary>
            /// <param name="httpClientInstance"> HttpClientInstance. </param>
            /// <param name="overrideHttpClientConfiguration"> OverrideHttpClientConfiguration. </param>
            /// <returns>Builder.</returns>
            public Builder HttpClientInstance(HttpClient httpClientInstance, bool overrideHttpClientConfiguration = true)
            {
                this.httpClientInstance = httpClientInstance ?? new HttpClient();
                this.overrideHttpClientConfiguration = overrideHttpClientConfiguration;
                return this;
            }

            /// <summary>
            /// Creates an object of the HttpClientConfiguration using the values provided for the builder.
            /// </summary>
            /// <returns>HttpClientConfiguration.</returns>
            public HttpClientConfiguration Build()
            {
                return new HttpClientConfiguration(
                        this.timeout,
                        this.httpClientInstance,
                        this.overrideHttpClientConfiguration);
            }
        }
    }
}