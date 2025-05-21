using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrisApi.JsonConverters;

public class IamFuckingDoneWithThisJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return true;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
        (JsonConverter) Activator.CreateInstance(
                typeof(IamFuckingDoneWithThisJsonConverter<>)
                .MakeGenericType([typeToConvert])
        );
}

public class IamFuckingDoneWithThisJsonConverter<T> : JsonConverter<T>
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) {
            return default;
        }

        if (reader.TokenType == JsonTokenType.StartArray) {
            if (reader.Read() && reader.TokenType == JsonTokenType.EndArray) {
                return default;
            }
            throw new JsonException();
        }

        return JsonSerializer.Deserialize<T>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
