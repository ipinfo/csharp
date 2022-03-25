using System.Text.Json;

using IPinfo.Models;

namespace IPinfo.Utilities
{
    public static class JsonHelper
    {
        private static readonly bool s_caseInsensitive = true;

        public static T Deserialize<T>(string json, JsonSerializerOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }
            
            if(options is null)
            {
                options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = s_caseInsensitive
                };                
            }

            return JsonSerializer.Deserialize<T>(json, options);                        
        }
        
        public static T Deserialize<T>(string json, bool caseInsensitive)
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
        public static string Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return JsonSerializer.Serialize(obj);
        }

        public static IPResponse ParseIPResponse(string response){
            IPResponse responseModel = JsonHelper.Deserialize<Models.IPResponse>(response);
            string[] latLongString = responseModel.Loc.Split(',');
            double [] latLong = new double[]{ double.Parse(latLongString[0]), double.Parse(latLongString[1])};

            responseModel.Latitude = latLong[0];
            responseModel.Longitude = latLong[1];
            responseModel.CountryName = CountryHelper.GetCountry(responseModel.Country);
            
            return responseModel;
        }
    }
}