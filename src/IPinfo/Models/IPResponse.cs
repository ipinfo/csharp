using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class IPResponse
  {
      //Non-public property accessors
      [JsonInclude]
      public bool Anycast { get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string City { get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string Country { get; private set; }
      
      public string CountryName { get; set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string Hostname { get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string Loc { get; private set; }

      public double Latitude { get; set; }

      public double Longitude { get; set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string Org { get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string Postal { get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string Region { get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string Timezone { get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public string IP { get; private set; }

      //Non-public property accessors
      [JsonInclude]
      public ASN Asn { get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public Company Company{ get; private set; }
      
      //TODO: Not available in response so far. Needs to update later.
      //Non-public property accessors
      [JsonInclude]
      public Carrier Carrier{ get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public Privacy Privacy{ get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public Abuse Abuse{ get; private set; }
      
      //Non-public property accessors
      [JsonInclude]
      public DomainsList Domains{ get; private set; }

  }
}