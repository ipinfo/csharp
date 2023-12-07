using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class DomainsList
  {
      public List<string> Domains { get; }
      public string IP { get; }
      public int Total { get; }

      // immutable type
      [JsonConstructor]
      public DomainsList(List<string> domains, string ip, int total) =>
            (Domains, IP, Total) = (domains, ip, total);
  }
}
