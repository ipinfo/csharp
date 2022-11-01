using System.Text.Json.Serialization;
namespace IPinfo.Models
{
  public class CountryFlag
  {
      [JsonInclude]
      public string Emoji { get; set;}
      [JsonInclude]
      public string Unicode { get; set;}

      // immutable type
      [JsonConstructor]
      public CountryFlag(string emoji, string unicode) =>
            (Emoji, Unicode) = (emoji, unicode);
  }
}
