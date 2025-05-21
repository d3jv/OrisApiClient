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

    [JsonConverter(typeof(IamFuckingDoneWithThisJsonConverterFactory))]
    public T? Data { get; init; }
}
