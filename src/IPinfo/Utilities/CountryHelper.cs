using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Collections.Generic;

namespace IPinfo.Utilities
{
    public static class CountryHelper
    {
        private const string COUTIRIES_JSON_FILE_PATH_NAME = "IPinfo.Utilities.Countries.json";

        private static Dictionary<string, string> countries = null;

        public static void Init()
        {
            if(countries == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                
                using (Stream stream = assembly.GetManifestResourceStream(COUTIRIES_JSON_FILE_PATH_NAME))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string countriesJson = reader.ReadToEnd();
                    countries = JsonSerializer.Deserialize<Dictionary<string, string>>(countriesJson);
                }
            }
        }

        public static string GetCountry(string countryCode)
        {
            if(countries.ContainsKey(countryCode))
            {
                return countries[countryCode];
            }
            else
            {
                return countryCode;
            }
        }
    }
}
