using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class Privacy
  {
      public bool Hosting { get; }
      public bool Proxy { get; }
      public bool Relay { get; }
      public string Service { get; }
      public bool Tor { get; }
      public bool Vpn { get; }

      // immutable type
      [JsonConstructor]
      public Privacy(bool hosting, bool proxy, bool relay, string service, bool tor, bool vpn) =>
            (Hosting, Proxy, Relay, Service, Tor, Vpn) = (hosting, proxy, relay, service, tor, vpn);
  }
}
