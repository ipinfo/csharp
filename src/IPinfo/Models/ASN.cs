using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class ASN
  {
      public string Asn { get; }
      public string Domain { get; }
      public string Name { get; }
      public string Route { get; }
      public string Type { get; }

      // immutable type
      [JsonConstructor]
      public ASN(string asn, string domain, string name, string route, string type) =>
            (Asn, Domain, Name, Route, Type) = (asn, domain, name, route, type);
  }
}
