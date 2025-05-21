using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrisApi.JsonConverters;

public class RetardedOrisResponseDataConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return Nullable.GetUnderlyingType(typeToConvert) is not null;
        // var k = typeToConvert.IsAssignableTo(typeof(IOrisResponseData));
        // Console.WriteLine($"Can Convert {typeToConvert}: {k}");
        // return k;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
        (JsonConverter)Activator.CreateInstance(
            typeof(NullableConverter<>)
                .MakeGenericType([Nullable.GetUnderlyingType(typeToConvert)!]),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: [options],
            culture: null)!;

    private class NullableConverter<T> : JsonConverter<T?>
    where T : struct
    {
        public NullableConverter(JsonSerializerOptions options)
        {
        }

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

            if (reader.TokenType == JsonTokenType.String &&
                    string.IsNullOrEmpty(reader.GetString())) {
                return default;
            }

            return JsonSerializer.Deserialize<T>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
            // throw new NotSupportedException("Writing is not supported due to a shortage of fucks given");
        }
    }
}
