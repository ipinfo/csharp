using System;
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
        private const string CountriesJsonFilePathName = "IPinfo.Utilities.Countries.json";
        private const string EuCountriesJsonFilePathName = "IPinfo.Utilities.EuCountries.json";

        /// <summary>
        /// Lazy initialization for the country dictionary object(only one instance) from country json file.
        /// </summary>
        private static readonly Lazy<Dictionary<string, string>> s_countries =
        new Lazy<Dictionary<string, string>>(() =>
        {
            Dictionary<string, string> countries;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(CountriesJsonFilePathName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string countriesJson = reader.ReadToEnd();
                countries = JsonSerializer.Deserialize<Dictionary<string, string>>(countriesJson);
            }
            return countries;
        });

        /// <summary>
        /// Lazy initialization for the EUcountry List from EUcountry json file.
        /// </summary>
        private static readonly Lazy<List<string>> eu_countries =
        new Lazy<List<string>>(() =>
        {
            List<string> _eu_countries;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(EuCountriesJsonFilePathName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string euCountriesJson = reader.ReadToEnd();
                _eu_countries = JsonSerializer.Deserialize<List<string>>(euCountriesJson);
            }
            return _eu_countries;
        });

        /// <summary>
        /// Dictionary for country_code to country_name mapping.
        /// </summary>
        private static Dictionary<string, string>  Countries { get { return s_countries.Value; } }
        
        /// <summary>
        /// List of EU countries.
        /// </summary>
        private static List<string>  EuCountries { get { return eu_countries.Value; } }

        /// <summary>
        /// Gets full country name against country code.
        /// </summary>
        /// <param name="countryCode">Country code consisting of two characters.</param>
        /// <returns>The full country name. If country code is not found in the dictionary, then same country code is returned.</returns>
        internal static string GetCountry(string countryCode)
        {
            if(countryCode == null)
            {
                return null;
            }
            
            if(Countries.ContainsKey(countryCode))
            {
                return Countries[countryCode];
            }
            else
            {
                return countryCode;
            }
        }

        /// <summary>
        /// whether a country is a member of the EU or not.
        /// </summary>
        /// <param name="countryCode">Country code consisting of two characters.</param>
        /// <returns>True if the country is a member of the European Union (EU) else false.</returns>
        internal static bool IsEU(string countryCode)
        {
            if(countryCode == null)
            {
                return false;
            }
            return EuCountries.Contains(countryCode);
        }
    }
}
