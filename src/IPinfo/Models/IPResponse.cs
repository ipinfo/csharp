namespace IPinfo.Models
{
  public class IPResponse
  {
      public bool Anycast { get; }
      public string City { get; }
      public string Country { get; }
      public string Hostname { get; }
      public string Loc { get; }
      public string Org { get; }
      public string Postal { get; }
      public string Region { get; }
      public string Timezone { get; }
      public string Ip { get; }

      public ASN Asn { get; }
      public Company Company{ get; }
      public Carrier Carrier{ get; }
      public Privacy Privacy{ get; }
      public Abuse Abuse{ get; }
      public DomainsList Domains{ get; }
      // TODO: [JsonIgnore]
      public Context Context{ get; }

  }
}