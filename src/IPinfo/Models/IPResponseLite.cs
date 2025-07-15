using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class IPResponseLite
  {
      [JsonInclude]
      public bool Anycast { get; private set; }

      [JsonInclude]
      public bool Bogon { get; internal set; }

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

      public Continent Continent { get; internal set; }

      [JsonInclude]
      public string IP { get; internal set; }

      [JsonInclude]
      public string Asn { get; private set; }

      [JsonPropertyName("as_name")]
      [JsonInclude]
      public string AsName {get; private set; }

      [JsonPropertyName("as_domain")]
      [JsonInclude]
      public string AsDomain {get; private set; }
  }
}
