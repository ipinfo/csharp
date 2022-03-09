using System.Text.Json.Serialization;

namespace IPinfo.Models
{
  public class Carrier
  {
      public string Mcc { get; }
      public string Mnc { get; }
      public string Name { get; }

      // immutable type
      [JsonConstructor]
      public Carrier(string mcc, string mnc, string name) =>
            (Mcc, Mnc, Name) = (mcc, mnc, name);
  }
}