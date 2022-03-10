using System.Text.Json;

using IPinfo.Models;

namespace IPinfo.Utilities
{
    
    public static class JsonHelper
    {
        private static readonly bool CaseInsensitive = true;

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
                    PropertyNameCaseInsensitive = CaseInsensitive
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
    }
}