using System.Text.Json;
using System.Text.Json.Serialization;

namespace BreakingNomad.Api.Helper;

public static class JsonSerializeHelper
{
  public static JsonSerializerOptions Default { get; } =
    new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
      Converters = { new JsonStringEnumConverter() }
    };

  public static JsonSerializerOptions DefaultIndented { get; } =
    new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
      Converters = { new JsonStringEnumConverter() },
      WriteIndented = true
    };
}