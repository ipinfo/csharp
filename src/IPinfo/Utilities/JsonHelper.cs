using System;
using System.Text.Json;

using IPinfo.Models;

namespace IPinfo.Utilities
{
    /// <summary>
    /// JsonHelper class contains json response parsing helper methods.
    /// </summary>
    internal static class JsonHelper
    {
        private const bool DefaultCaseInsensitive = true;
        private const string CountryFlagURL = "https://cdn.ipinfo.io/static/images/countries-flags/";

        /// <summary>
        /// JSON Deserialization of a given json string. Case Insensitivity is set to true if options is null.
        /// </summary>
        /// <param name="json">The json string to be deserialize into object.</param>
        /// <param name="options">The options to be used for deserialization.</param>
        /// <returns>The deserialized object.</returns>
        internal static T Deserialize<T>(string json, JsonSerializerOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

            if(options is null)
            {
                options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = DefaultCaseInsensitive
                };
            }

            return JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// JSON Deserialization of a given json string.
        /// </summary>
        /// <param name="json">The json string to be deserialize into object.</param>
        /// <param name="caseInsensitive">The boolean options if Property Names should be case insensitive.</param>
        /// <returns>The deserialized object.</returns>
        internal static T Deserialize<T>(string json, bool caseInsensitive)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = caseInsensitive
            };

            return Deserialize<T>(json, options);
        }

        /// <summary>
        /// JSON Serialization of a given object.
        /// </summary>
        /// <param name="obj">The object to serialize into JSON.</param>
        /// <returns>The serialized Json string representation of the given object.</returns>
        internal static string Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// IPResponse object with extra manual parsing.
        /// </summary>
        /// <param name="response">The json string to be parsed.</param>
        /// <returns>The deserialized IPResponse object with extra parsing for latitude, longitude, and country being done.</returns>
        internal static IPResponse ParseIPResponse(string response){
            IPResponse responseModel = JsonHelper.Deserialize<Models.IPResponse>(response);

            if(!String.IsNullOrEmpty(responseModel.Loc))
            {
                // splitting loc string in "latitude,longitude" fromat
                string[] latLongString = responseModel.Loc.Split(',');
                if(latLongString.Length == 2)
                {
                    responseModel.Latitude = latLongString[0];
                    responseModel.Longitude = latLongString[1];
                }
            }

            responseModel.CountryName = CountryHelper.GetCountry(responseModel.Country);
            responseModel.IsEU = CountryHelper.IsEU(responseModel.Country);
            responseModel.CountryFlag = CountryHelper.GetCountryFlag(responseModel.Country);
            responseModel.CountryCurrency = CountryHelper.GetCountryCurrency(responseModel.Country);
            responseModel.Continent = CountryHelper.GetContinent(responseModel.Country);
            responseModel.CountryFlagURL = CountryFlagURL + responseModel.Country + ".svg";

            return responseModel;
        }

        /// <summary>
        /// IPResponseLite object with extra manual parsing.
        /// </summary>
        /// <param name="response">The json string to be parsed.</param>
        /// <returns>The deserialized IPResponseLite object with extra parsing for country being done.</returns>
        internal static IPResponseLite ParseIPResponseLite(string response) {
            IPResponseLite responseModel = JsonHelper.Deserialize<Models.IPResponseLite>(response);
            responseModel.CountryName = CountryHelper.GetCountry(responseModel.CountryCode);
            responseModel.IsEU = CountryHelper.IsEU(responseModel.CountryCode);
            responseModel.CountryFlag = CountryHelper.GetCountryFlag(responseModel.CountryCode);
            responseModel.CountryCurrency = CountryHelper.GetCountryCurrency(responseModel.CountryCode);
            responseModel.Continent = CountryHelper.GetContinent(responseModel.CountryCode);
            responseModel.CountryFlagURL = CountryFlagURL + responseModel.CountryCode + ".svg";
            return responseModel;
        }

        /// <summary>
        /// IPResponseCore object with extra manual parsing.
        /// </summary>
        /// <param name="response">The json string to be parsed.</param>
        /// <returns>The deserialized IPResponseCore object with extra parsing for geo object country enrichment being done.</returns>
        internal static IPResponseCore ParseIPResponseCore(string response) {
            IPResponseCore responseModel = JsonHelper.Deserialize<Models.IPResponseCore>(response);

            if (responseModel.Geo != null && !String.IsNullOrEmpty(responseModel.Geo.CountryCode))
            {
                responseModel.Geo.CountryName = CountryHelper.GetCountry(responseModel.Geo.CountryCode);
                responseModel.Geo.IsEU = CountryHelper.IsEU(responseModel.Geo.CountryCode);
                responseModel.Geo.CountryFlag = CountryHelper.GetCountryFlag(responseModel.Geo.CountryCode);
                responseModel.Geo.CountryCurrency = CountryHelper.GetCountryCurrency(responseModel.Geo.CountryCode);
                responseModel.Geo.ContinentInfo = CountryHelper.GetContinent(responseModel.Geo.CountryCode);
                responseModel.Geo.CountryFlagURL = CountryFlagURL + responseModel.Geo.CountryCode + ".svg";
            }

            return responseModel;
        }
    }
}
