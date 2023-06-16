using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bumbershoot.Utilities.Helpers;
using Google.Protobuf.Collections;

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
      Converters = { new JsonStringEnumConverter(), new RepeatedFieldConverterFactory() }
    };
  }

  public class RepeatedFieldConverterFactory : JsonConverterFactory
  {
    public override bool CanConvert(Type typeToConvert)
    {
      if (!typeToConvert.IsGenericType) return false;

      if (typeToConvert.GetGenericTypeDefinition() != typeof(RepeatedField<>)) return false;


      return true;
    }

    public override JsonConverter CreateConverter(
      Type type,
      JsonSerializerOptions options)
    {
      var itemType = type.GetGenericArguments()[0];

      var converter = (JsonConverter)Activator.CreateInstance(
        typeof(RepeatedFieldConverter<>).MakeGenericType(itemType),
        BindingFlags.Instance | BindingFlags.Public,
        null,
        new object[] { options },
        null)!;

      return converter;
    }

    private class RepeatedFieldConverter<TItem> :
      JsonConverter<RepeatedField<TItem>>
    {
      private readonly JsonConverter<TItem> _valueConverter;
      private readonly Type _valueType;

      public RepeatedFieldConverter(JsonSerializerOptions options)
      {
        _valueConverter = (JsonConverter<TItem>)options
          .GetConverter(typeof(TItem));
        _valueType = typeof(TItem);
      }

      public override RepeatedField<TItem> Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
      {
        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

        var repeatedField = new RepeatedField<TItem>();

        while (reader.Read())
        {
          if (reader.TokenType == JsonTokenType.EndArray) return repeatedField;

          var value = _valueConverter.Read(ref reader, _valueType, options)!;
          repeatedField.Add(value);
        }


        throw new JsonException();
      }


      public override void Write(
        Utf8JsonWriter writer,
        RepeatedField<TItem> repatedField,
        JsonSerializerOptions options)
      {
        writer.WriteStartArray();

        foreach (var value in repatedField) _valueConverter.Write(writer, value, options);

        writer.WriteEndArray();
      }
    }
  }
}
