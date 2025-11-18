using System.Text.Json.Serialization;

namespace IPinfo.Models
{
    public class IPResponseCore
    {
        [JsonInclude]
        public string IP { get; internal set; }

        [JsonInclude]
        public string Hostname { get; private set; }

        [JsonInclude]
        public bool Bogon { get; internal set; }

        [JsonInclude]
        public GeoCore Geo { get; private set; }

        [JsonPropertyName("as")]
        [JsonInclude]
        public ASCore As { get; private set; }

        [JsonInclude]
        public object Mobile { get; private set; }

        [JsonInclude]
        public AnonymousCore Anonymous { get; private set; }

        [JsonPropertyName("is_anonymous")]
        [JsonInclude]
        public bool IsAnonymous { get; private set; }

        [JsonPropertyName("is_anycast")]
        [JsonInclude]
        public bool IsAnycast { get; private set; }

        [JsonPropertyName("is_hosting")]
        [JsonInclude]
        public bool IsHosting { get; private set; }

        [JsonPropertyName("is_mobile")]
        [JsonInclude]
        public bool IsMobile { get; private set; }

        [JsonPropertyName("is_satellite")]
        [JsonInclude]
        public bool IsSatellite { get; private set; }
    }

    public class GeoCore
    {
        [JsonInclude]
        public string City { get; private set; }

        [JsonInclude]
        public string Region { get; private set; }

        [JsonPropertyName("region_code")]
        [JsonInclude]
        public string RegionCode { get; private set; }

        [JsonInclude]
        public string Country { get; private set; }

        [JsonPropertyName("country_code")]
        [JsonInclude]
        public string CountryCode { get; private set; }

        public string CountryName { get; internal set; }

        public bool IsEU { get; internal set; }

        public CountryFlag CountryFlag { get; internal set; }

        public string CountryFlagURL { get; internal set; }

        public CountryCurrency CountryCurrency { get; internal set; }

        [JsonInclude]
        public string Continent { get; private set; }

        [JsonPropertyName("continent_code")]
        [JsonInclude]
        public string ContinentCode { get; private set; }

        public Continent ContinentInfo { get; internal set; }

        [JsonInclude]
        public double Latitude { get; private set; }

        [JsonInclude]
        public double Longitude { get; private set; }

        [JsonInclude]
        public string Timezone { get; private set; }

        [JsonPropertyName("postal_code")]
        [JsonInclude]
        public string PostalCode { get; private set; }

        [JsonPropertyName("dma_code")]
        [JsonInclude]
        public string DmaCode { get; private set; }

        [JsonPropertyName("geoname_id")]
        [JsonInclude]
        public string GeonameId { get; private set; }

        [JsonInclude]
        public int Radius { get; private set; }
    }

    public class ASCore
    {
        [JsonInclude]
        public string Asn { get; private set; }

        [JsonInclude]
        public string Name { get; private set; }

        [JsonInclude]
        public string Domain { get; private set; }

        [JsonInclude]
        public string Type { get; private set; }

        [JsonPropertyName("last_changed")]
        [JsonInclude]
        public string LastChanged { get; private set; }
    }

    public class AnonymousCore
    {
        [JsonPropertyName("is_proxy")]
        [JsonInclude]
        public bool IsProxy { get; private set; }

        [JsonPropertyName("is_relay")]
        [JsonInclude]
        public bool IsRelay { get; private set; }

        [JsonPropertyName("is_tor")]
        [JsonInclude]
        public bool IsTor { get; private set; }

        [JsonPropertyName("is_vpn")]
        [JsonInclude]
        public bool IsVpn { get; private set; }
    }
}
