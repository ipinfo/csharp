using System.Text.Json.Serialization;
namespace IPinfo.Models
{
  /// <summary>
  /// Gets country flag emoji and unicode.
  /// {Emoji:"🇵🇰", Unicode:"U+1F1F5 U+1F1F0"}
  /// </summary>
  public class CountryFlag
  {
      [JsonInclude]
      public string Emoji { get; set; }

      [JsonInclude]
      public string Unicode { get; set; }
  }
}
