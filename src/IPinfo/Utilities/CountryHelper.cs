using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Collections.Generic;

namespace IPinfo.Utilities
{
    public static class CountryHelper
    {
        private const string ConutriesJsonFilePathName = "IPinfo.Utilities.Countries.json";

        // There will be only one instance of country dictionary.
        private static Dictionary<string, string> s_countries = null;

        // Init needs to be called at start, e.g. in constructor of IPinfoClient.
        public static void Init()
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

        public static string GetCountry(string countryCode)
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