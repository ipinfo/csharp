using System.Text.Json.Serialization;
namespace IPinfo.Models
{
  /// <summary>
  /// Gets continent code and name.
  /// { "code": "AS", "name": "Asia"}
  /// </summary>
  public class Continent
  {
      [JsonInclude]
      public string Code { get; private set; }

      [JsonInclude]
      public string Name { get; private set; }
  }
}
