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
    /// IPApiLite.
    /// </summary>
    public sealed class IPApiLite : BaseApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPApiLite"/> class.
        /// </summary>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="token"> token. </param>
        internal IPApi(IHttpClient httpClient, string token, CacheHandler cacheHandler)
            : base(httpClient, token, cacheHandler)
        {
            this.BaseUrl = "https://api.ipinfo.io/lite/"
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <returns>Returns the Models.IPResponseLite response from the API call.</returns>
        public Models.IPResponseLite GetDetails(
                IPAddress ipAddress)
        {
            string ipString = ipAddress?.ToString();
            return this.GetDetails(ipString);
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <returns>Returns the Models.IPResponseLite response from the API call.</returns>
        public Models.IPResponseLite GetDetails(
                string ipAddress = "")
        {
            Task<Models.IPResponseLite> t = this.GetDetailsAsync(ipAddress);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <param name="cancellationToken">Cancellation token if the request is cancelled. </param>
        /// <returns>Returns the Models.IPResponseLite response from the API call.</returns>
        public Task<Models.IPResponseLite> GetDetailsAsync(
                IPAddress ipAddress,
                CancellationToken cancellationToken = default)
        {
            string ipString = ipAddress?.ToString();
            return this.GetDetailsAsync(ipString, cancellationToken);
        }


        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <param name="cancellationToken">Cancellation token if the request is cancelled. </param>
        /// <returns>Returns the Models.IPResponseLite response from the API call.</returns>
        public async Task<Models.IPResponseLite> GetDetailsAsync(
                string ipAddress = "",
                CancellationToken cancellationToken = default)
        {
            if (ipAddress == null)
            {
                ipAddress = "";
            }
            // first check the data in the cache if cache is available
            IPResponseLite ipResponse = (IPResponseLite)GetFromCache(ipAddress);
            if (ipResponse != null)
            {
                return ipResponse;
            }

            if (BogonHelper.IsBogon(ipAddress))
            {
                ipResponse = new IPResponseLite()
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

            var responseModel = JsonHelper.ParseIPResponseLite(response.Body);

            SetInCache(ipAddress, responseModel);
            return responseModel;
        }
    }
}
