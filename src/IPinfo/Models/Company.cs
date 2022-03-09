using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class Company
  {
      public string Domain { get; }
      public string Name { get; }
      public string Type { get; }

      // immutable type
      [JsonConstructor]
      public Company(string domain, string name, string type) =>
            (Domain, Name, Type) = (domain, name, type);
  }
}