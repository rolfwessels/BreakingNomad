using System.Text.Json;
using System.Text.Json.Serialization;
using Bumbershoot.Utilities.Helpers;

namespace BreakingNomad.Api.Helper;

public static class JsonSerializeHelper
{
  public static JsonSerializerOptions Default { get; } =
    Init();

  public static JsonSerializerOptions DefaultIndented { get; } = Init().With(x => x.WriteIndented = true);

  private static JsonSerializerOptions Init()
  {
    return new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
      Converters = { new JsonStringEnumConverter() }
    };
  }
  
}
