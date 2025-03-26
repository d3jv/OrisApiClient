using System.Text.Json.Serialization;
using static OrisApi.Models.OrisAuth;

namespace OrisApi.Models;

public class OrisAuth : Dictionary<string, InnerOrisAuth>
{
    [JsonIgnore]
    public int? ID => this.FirstOrDefault().Value.ID;

    public class InnerOrisAuth {
        [JsonInclude]
        [JsonPropertyName("ID")]
        required public string _id { private get; init; }

        [JsonIgnore]
        public int? ID => int.Parse(_id);
    }
}
