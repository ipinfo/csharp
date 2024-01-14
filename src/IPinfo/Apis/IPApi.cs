using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using IPinfo.Utilities;
using IPinfo.Http.Client;
using IPinfo.Http.Request;
using IPinfo.Models;
using IPinfo.Http.Response;
using IPinfo.Cache;

namespace IPinfo.Apis
{
    /// <summary>
    /// IPApi.
    /// </summary>
    public sealed class IPApi : BaseApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPApi"/> class.
        /// </summary>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="token"> token. </param>
        internal IPApi(IHttpClient httpClient, string token, CacheHandler cacheHandler)
            : base(httpClient, token, cacheHandler)
        {
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <returns>Returns the Models.IPResponse response from the API call.</returns>
        public Models.IPResponse GetDetails(
                IPAddress ipAddress)
        {
            string ipString = ipAddress?.ToString();
            return this.GetDetails(ipString);
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <returns>Returns the Models.IPResponse response from the API call.</returns>
        public Models.IPResponse GetDetails(
                string ipAddress = "")
        {
            Task<Models.IPResponse> t = this.GetDetailsAsync(ipAddress);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Retrieves details of an IPv6 address.
        /// </summary>
        /// <param name="ipv6Address">The IPv6 address of the user to retrieve details for.</param>
        /// <returns>Returns the Models.IPResponse response from the API call.</returns>
        public Models.IPResponse GetDetailsV6(IPAddress ipv6Address)
        {
            string ipString = ipv6Address?.ToString();
            return this.GetDetailsV6(ipString);
        }

        /// <summary>
        /// Retrieves details of an IPv6 address.
        /// </summary>
        /// <param name="ipv6Address">The IPv6 address of the user to retrieve details for.</param>
        /// <returns>Returns the Models.IPResponse response from the API call.</returns>
        public Models.IPResponse GetDetailsV6(string ipv6Address)
        {
            Task<Models.IPResponse> t = this.GetDetailsV6Async(ipv6Address);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <param name="cancellationToken">Cancellation token if the request is cancelled. </param>
        /// <returns>Returns the Models.IPResponse response from the API call.</returns>
        public Task<Models.IPResponse> GetDetailsAsync(
                IPAddress ipAddress,
                CancellationToken cancellationToken = default)
        {
            string ipString = ipAddress?.ToString();
            return this.GetDetailsAsync(ipString, cancellationToken);
        }

        /// <summary>
        /// Retrieves details of an IPv6 address.
        /// </summary>
        /// <param name="ipv6Address">The IPv6 address of the user to retrieve details for.</param>
        /// <param name="cancellationToken">Cancellation token if the request is cancelled. </param>
        /// <returns>Returns the Models.IPResponse response from the API call.</returns>
        public Task<Models.IPResponse> GetDetailsV6Async(
        IPAddress ipv6Address,
        CancellationToken cancellationToken = default)
        {
            string ipString = ipv6Address?.ToString();
            return this.GetDetailsV6Async(ipString, cancellationToken);
        }


        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <param name="cancellationToken">Cancellation token if the request is cancelled. </param>
        /// <returns>Returns the Models.IPResponse response from the API call.</returns>
        public async Task<Models.IPResponse> GetDetailsAsync(
                string ipAddress = "",
                CancellationToken cancellationToken = default)
        {
            if (ipAddress == null)
            {
                ipAddress = "";
            }
            // first check the data in the cache if cache is available
            IPResponse ipResponse = (IPResponse)GetFromCache(ipAddress);
            if (ipResponse != null)
            {
                return ipResponse;
            }

            if (BogonHelper.IsBogon(ipAddress))
            {
                ipResponse = new IPResponse()
                {
                    IP = ipAddress,
                    Bogon = true
                };
                return ipResponse;
            }

            // Determine the base URI based on IP version
            string baseUri = this.BaseUrl;

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("{ip_address}");
            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "ip_address", ipAddress },
            });

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.CreateGetRequest(queryBuilder.ToString());

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            // handle errors defined at the API level.
            this.ValidateResponse(context);

            var responseModel = JsonHelper.ParseIPResponse(response.Body);

            SetInCache(ipAddress, responseModel);
            return responseModel;
        }

        /// <summary>
        /// Retrieves details of an IPv6 address.
        /// </summary>
        /// <param name="ipv6Address">The IPv6 address of the user to retrieve details for.</param>
        /// <param name="cancellationToken">Cancellation token if the request is cancelled. </param>
        /// <returns>Returns the Models.IPResponse response from the API call.</returns>
        public async Task<Models.IPResponse> GetDetailsV6Async(
        string ipv6Address,
        CancellationToken cancellationToken = default)
        {
            if (ipv6Address == null)
            {
                ipv6Address = "";
            }
            // Check the data in the cache if cache is available
            IPResponse ipResponse = (IPResponse)GetFromCache(ipv6Address);
            if (ipResponse != null)
            {
                return ipResponse;
            }

            if (BogonHelper.IsBogon(ipv6Address))
            {
                ipResponse = new IPResponse()
                {
                    IP = ipv6Address,
                    Bogon = true
                };
                return ipResponse;
            }

            // Use the IPv6 base URI for API requests.
            string baseUri = this.BaseUrlIPv6;

            // Prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("{ip_address}");
            // Process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "ip_address", ipv6Address },
            });

            // Prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.CreateGetRequest(queryBuilder.ToString());

            // Invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            // Handle errors defined at the API level.
            this.ValidateResponse(context);

            var responseModel = JsonHelper.ParseIPResponse(response.Body);

            SetInCache(ipv6Address, responseModel);
            return responseModel;
        }

    }
}
