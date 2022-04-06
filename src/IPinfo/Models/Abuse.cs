using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class Abuse
  {
      public string Address { get; }
      public string Country { get; }
      public string Email { get; }
      public string Name { get; }
      public string Network { get; }
      public string Phone { get; }

      // immutable type
      [JsonConstructor]
      public Abuse(string address, string country, string email, string name, string network, string phone) =>
            (Address, Country, Email, Name, Network, Phone) = (address, country, email, name, network, phone);
  }
}
