using System.Text.Json.Serialization;

namespace IPinfo.Models
{
    /// <summary>
    /// Residential proxy detection response.
    /// </summary>
    public class IPResponseResproxy
    {
        /// <summary>
        /// The IP address.
        /// </summary>
        [JsonPropertyName("ip")]
        public string IP { get; set; }

        /// <summary>
        /// The last time this IP was seen as a residential proxy.
        /// </summary>
        [JsonPropertyName("last_seen")]
        public string LastSeen { get; set; }

        /// <summary>
        /// The percentage of days seen as a residential proxy.
        /// </summary>
        [JsonPropertyName("percent_days_seen")]
        public double? PercentDaysSeen { get; set; }

        /// <summary>
        /// The residential proxy service name.
        /// </summary>
        [JsonPropertyName("service")]
        public string Service { get; set; }

        // immutable type
        [JsonConstructor]
        public IPResponseResproxy(string ip, string lastSeen, double? percentDaysSeen, string service) =>
            (IP, LastSeen, PercentDaysSeen, Service) = (ip, lastSeen, percentDaysSeen, service);
    }
}
