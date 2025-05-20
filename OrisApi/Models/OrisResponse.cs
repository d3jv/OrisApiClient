using System.Text.Json.Serialization;
using OrisApi.JsonConverters;

namespace OrisApi.Models;

public class OrisResponse<T>
{
    public string Method { get; init; }

    public string Format { get; init; }

    public string Status { get; init; }

    [JsonIgnore]
    public bool IsOk => Status == "OK";

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
    [JsonConverter(typeof(RetardedOrisResponseDataConverter))]
    public T? Data { get; init; }
}
