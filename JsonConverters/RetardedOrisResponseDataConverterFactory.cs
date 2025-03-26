using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrisApi.JsonConverters;

public class RetardedOrisResponseDataConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType &&
            typeToConvert.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
            typeToConvert.GetGenericArguments()[0] == typeof(string);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type[] typeArguments = typeToConvert.GetGenericArguments();
        Type keyType = typeArguments[0];
        Debug.Assert(keyType == typeof(string));
        Type valueType = typeArguments[1];

        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
            typeof(DictionaryStringConverter<>).MakeGenericType(
                [valueType]),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: [options],
            culture: null)!;

        return converter;
    }

    private class DictionaryStringConverter<Tvalue> : JsonConverter<Dictionary<string, Tvalue>?>
    {
        private readonly JsonConverter<Tvalue> _valueConverter;
        private readonly Type _valueType;

        public DictionaryStringConverter(JsonSerializerOptions options)
        {
            // For performance, use the existing converter.
            _valueConverter = (JsonConverter<Tvalue>)options
                .GetConverter(typeof(Tvalue));

            // Cache the value type.
            _valueType = typeof(Tvalue);
        }

        public override Dictionary<string, Tvalue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray) {
                if (reader.Read() && reader.TokenType == JsonTokenType.EndArray) {
                    return null;
                }
                throw new JsonException();
            }

            if (reader.TokenType != JsonTokenType.StartObject) {
                throw new JsonException();
            }

            var dictionary = new Dictionary<string, Tvalue>();

            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndObject) {
                    return dictionary;
                }

                // Get the key.
                if (reader.TokenType != JsonTokenType.PropertyName) {
                    throw new JsonException();
                }

                string? propertyName = reader.GetString();
                if (propertyName == null) {
                    throw new JsonException();
                }

                // Get the value.
                reader.Read();
                Tvalue value = _valueConverter.Read(ref reader, _valueType, options)!;

                // Add to dictionary.
                dictionary.Add(propertyName, value);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, Tvalue>? value, JsonSerializerOptions options)
        {
            throw new NotSupportedException("Writing is not supported due to a shortage of fucks given");
        }
    }
}
