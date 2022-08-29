using Newtonsoft.Json;

namespace Services.Commons
{
    public class LoginModel
    {
        [JsonProperty("data")]
        public AuthToken AuthToken { get; set; }

        [JsonProperty("errType")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonIgnore]
        public string StorageToken { get; set; }
    }
}
