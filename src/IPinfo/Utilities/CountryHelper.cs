using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Collections.Generic;

namespace IPinfo.Utilities
{
    /// <summary>
    /// CountryHelper class contains country parsing helper methods.
    /// </summary>
    internal static class CountryHelper
    {
        private const string ConutriesJsonFilePathName = "IPinfo.Utilities.Countries.json";

        // There will be only one instance of country dictionary.
        private static Dictionary<string, string> s_countries = null;

        /// <summary>
        /// Initializes the country dictionary object from country json file. Should be initialized before using other methods.
        /// </summary>
        internal static void Init()
        {
            if(s_countries == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                
                using (Stream stream = assembly.GetManifestResourceStream(ConutriesJsonFilePathName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string countriesJson = reader.ReadToEnd();
                    s_countries = JsonSerializer.Deserialize<Dictionary<string, string>>(countriesJson);
                }
            }
        }

        /// <summary>
        /// Gets full country name against country code.
        /// </summary>
        /// <param name="countryCode">Country code consisting of two characters.</param>
        /// <returns>The full country name. If country code is not found in the dictionary, then same country code is returned.</returns>
        internal static string GetCountry(string countryCode)
        {
            if(s_countries.ContainsKey(countryCode))
            {
                return s_countries[countryCode];
            }
            else
            {
                return countryCode;
            }
        }
    }
}