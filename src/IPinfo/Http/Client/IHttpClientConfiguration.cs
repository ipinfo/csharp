using System;
using System.Net.Http;

namespace IPinfo.Http.Client
{
    /// <summary>
    /// Represents the current state of the Http Client.
    /// </summary>
    public interface IHttpClientConfiguration
    {
        /// <summary>
        /// Http client timeout.
        /// </summary>
        TimeSpan Timeout { get; }

        HttpClient HttpClientInstance { get; }

        /// <summary>
        /// Boolean which allows the SDK to override http client instance's settings used for features like retries, timeouts etc.
        /// </summary>
        bool OverrideHttpClientConfiguration { get; }
    }
}