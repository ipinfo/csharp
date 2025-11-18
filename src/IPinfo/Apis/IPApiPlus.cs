using System.Net;
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
    /// IPApiPlus.
    /// </summary>
    public sealed class IPApiPlus : BaseApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPApiPlus"/> class.
        /// </summary>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="token"> token. </param>
        /// <param name="cacheHandler"> cacheHandler. </param>
        internal IPApiPlus(IHttpClient httpClient, string token, CacheHandler cacheHandler)
            : base(httpClient, token, cacheHandler)
        {
            this.BaseUrl = "https://api.ipinfo.io/lookup/";
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <returns>Returns the Models.IPResponsePlus response from the API call.</returns>
        public Models.IPResponsePlus GetDetails(
                IPAddress ipAddress)
        {
            string ipString = ipAddress?.ToString();
            return this.GetDetails(ipString);
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <returns>Returns the Models.IPResponsePlus response from the API call.</returns>
        public Models.IPResponsePlus GetDetails(
                string ipAddress = "")
        {
            Task<Models.IPResponsePlus> t = this.GetDetailsAsync(ipAddress);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Retrieves details of an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the user to retrieve details for.</param>
        /// <param name="cancellationToken">Cancellation token if the request is cancelled. </param>
        /// <returns>Returns the Models.IPResponsePlus response from the API call.</returns>
        public Task<Models.IPResponsePlus> GetDetailsAsync(
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
        /// <returns>Returns the Models.IPResponsePlus response from the API call.</returns>
        public async Task<Models.IPResponsePlus> GetDetailsAsync(
                string ipAddress = "",
                CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = "";
            }

            // first check the data in the cache if cache is available
            IPResponsePlus ipResponse = (IPResponsePlus)GetFromCache(ipAddress);
            if (ipResponse != null)
            {
                return ipResponse;
            }

            if (BogonHelper.IsBogon(ipAddress))
            {
                ipResponse = new IPResponsePlus()
                {
                    IP = ipAddress,
                    Bogon = true
                };
                return ipResponse;
            }

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.CreateGetRequest(this.BaseUrl + ipAddress);
            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            // handle errors defined at the API level.
            this.ValidateResponse(context);

            var responseModel = JsonHelper.ParseIPResponsePlus(response.Body);

            SetInCache(ipAddress, responseModel);
            return responseModel;
        }
    }
}
