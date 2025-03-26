using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrisApi.JsonConverters;

public class RetardedOrisResponseDataConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return true;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
            typeof(NullableConverter<>).MakeGenericType(
                [typeToConvert]),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: [options],
            culture: null)!;

        return converter;
    }

    private class NullableConverter<T> : JsonConverter<T?>
    {
        private readonly JsonConverter<T?> _innerConverter;
        private readonly Type _type;

        public NullableConverter(JsonSerializerOptions options)
        {
            _type = typeof(T);
            
            // For performance, use the existing converter.
            _innerConverter = (JsonConverter<T?>)options
                .GetConverter(_type);
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray) {
                if (reader.Read() && reader.TokenType == JsonTokenType.EndArray) {
                    return default;
                }
                throw new JsonException();
            }

            return _innerConverter.Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            throw new NotSupportedException("Writing is not supported due to a shortage of fucks given");
        }
    }
}
