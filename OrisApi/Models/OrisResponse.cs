using System.Text.Json.Serialization;

namespace OrisApi.Models;

public class OrisResponse<T>
where T : IOrisResponseData
{
    public string Method { get; init; }

    public string Format { get; init; }

    public string Status { get; init; }

    [JsonIgnore]
    public bool IsOk => Status == "OK";

    public DateTime ExportCreated { get; init; }

    public T? Data { get; init; }
}
