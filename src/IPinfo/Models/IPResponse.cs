using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class IPResponse
  {
      [JsonInclude]
      public bool Anycast { get; private set; }
      
      [JsonInclude]
      public bool Bogon { get; private set; }
      
      [JsonInclude]
      public string City { get; private set; }
      
      [JsonInclude]
      public string Country { get; private set; }
      
      public string CountryName { get; internal set; }

      public bool IsEU { get; internal set; }
      
      public CountryFlag CountryFlag { get; internal set; }

      public CountryCurrency CountryCurrency { get; internal set; }

      public Continent Continent { get; internal set; }

      [JsonInclude]
      public string Hostname { get; private set; }
      
      [JsonInclude]
      public string Loc { get; private set; }

      public string Latitude { get; internal set; }

      public string Longitude { get; internal set; }
      
      [JsonInclude]
      public string Org { get; private set; }
      
      [JsonInclude]
      public string Postal { get; private set; }
      
      [JsonInclude]
      public string Region { get; private set; }
      
      [JsonInclude]
      public string Timezone { get; private set; }
      
      [JsonInclude]
      public string IP { get; private set; }

      [JsonInclude]
      public ASN Asn { get; private set; }
      
      [JsonInclude]
      public Company Company{ get; private set; }
      
      [JsonInclude]
      public Carrier Carrier{ get; private set; }
      
      [JsonInclude]
      public Privacy Privacy{ get; private set; }
      
      [JsonInclude]
      public Abuse Abuse{ get; private set; }
      
      [JsonInclude]
      public DomainsList Domains{ get; private set; }
  }
}
