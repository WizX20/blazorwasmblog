using JsonProperty = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace BlazorWasmBlog.Modules.SquidexCms.Models
{
    public class LoginResultModel
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public long Expires { get; set; }
    }
}