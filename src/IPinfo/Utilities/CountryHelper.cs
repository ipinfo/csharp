using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Collections.Generic;

using IPinfo.Models;

namespace IPinfo.Utilities
{
    /// <summary>
    /// CountryHelper class contains country parsing helper methods.
    /// </summary>
    internal static class CountryHelper
    {
        private const string CountriesJsonFilePathName = "IPinfo.Utilities.Countries.json";
        private const string EUCountriesJsonFilePathName = "IPinfo.Utilities.EUCountries.json";
        private const string CountriesFlagsJsonFilePathName = "IPinfo.Utilities.Flags.json";

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
        /// Lazy initialization for the countryFlag dictionary object from flags json file.
        /// </summary>
        private static readonly Lazy<Dictionary<string, CountryFlag>> s_countriesFlags =
        new Lazy<Dictionary<string, CountryFlag>>(() =>
        {
            Dictionary<string, CountryFlag> countriesFlags;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(CountriesFlagsJsonFilePathName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string countriesFlagsJson = reader.ReadToEnd();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };     
                countriesFlags = JsonSerializer.Deserialize<Dictionary<string, CountryFlag>>(countriesFlagsJson, options);
            }
            return countriesFlags;
        });

        /// <summary>
        /// Lazy initialization for the EUcountry List from EUcountry json file.
        /// </summary>
        private static readonly Lazy<List<string>> s_euCountries =
        new Lazy<List<string>>(() =>
        {
            List<string> euCountries;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(EUCountriesJsonFilePathName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string euCountriesJson = reader.ReadToEnd();
                euCountries = JsonSerializer.Deserialize<List<string>>(euCountriesJson);
            }
            return euCountries;
        });

        /// <summary>
        /// Dictionary for country_code to country_name mapping.
        /// </summary>
        private static Dictionary<string, string>  Countries { get { return s_countries.Value; } }

        /// <summary>
        /// Dictionary for country_code to country_flag mapping.
        /// </summary>
        private static Dictionary<string, CountryFlag>  CountriesFlags { get { return s_countriesFlags.Value; } }
        
        /// <summary>
        /// List of EU countries.
        /// </summary>
        private static List<string>  EUCountries { get { return s_euCountries.Value; } }

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
            return EUCountries.Contains(countryCode);
        }

        /// <summary>
        /// Gets country flag against country code.
        /// "PK" -> CountryFlag:{Emoji:"🇵🇰", Unicode:"U+1F1F5 U+1F1F0"}
        /// </summary>
        /// <param name="countryCode">Country code consisting of two characters.</param>
        /// <returns>CountryFlag of the country.</returns>
        internal static CountryFlag GetCountryFlag(string countryCode)
        {
            if(countryCode == null)
            {
                return null;
            }
            
            if(CountriesFlags.ContainsKey(countryCode))
            {
                return CountriesFlags[countryCode];
            }
            else
            {
                return null;
            }
        }
    }
}
