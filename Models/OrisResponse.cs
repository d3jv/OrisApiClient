using System.Text.Json.Serialization;
using OrisApi.JsonConverters;

namespace OrisApi.Models;

public class OrisResponse<T>
{
    public string Method { get; init; }

    public string Format { get; init; }

    public string Status { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime ExportCreated { get; init; }

    /// <summary>
    /// The oris API is retarded (hence the converter name).
    ///
    /// When the response is successful, the data looks something like this:
    /// { "foo": "value", "bar": "value" }
    /// On the other hand, not found looks like this:
    /// []
    /// IT'S AN EMPTY ARRAY, a whole different type. The retarded data converter handles this.
    /// </summary>
    [JsonInclude] // Allows serialization of props with private getter
    [JsonPropertyName("Data")]
    [JsonConverter(typeof(RetardedOrisResponseDataConverter))]
    // TODO: dictionary is returned only when multiple entries are requested (like getCsosClubList)
    //       We should probably fix this
    public IDictionary<string, T>? _data { private get; init; }

    [JsonIgnore]
    public IEnumerable<T>? Data => _data?.Values;
}
