using System.Text.Json.Serialization;
namespace IPinfo.Models
{
  /// <summary>
  /// Gets country currency code and symbol.
  /// { "code": "PKR" ,"symbol": "₨"}
  /// </summary>
  public class CountryCurrency
  {
      [JsonInclude]
      public string Code { get; internal set; }

      [JsonInclude]
      public string Symbol { get; internal set; }
  }
}
