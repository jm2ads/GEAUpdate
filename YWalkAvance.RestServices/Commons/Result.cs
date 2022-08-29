using Newtonsoft.Json;

namespace Services.Commons
{
    [JsonObject]
    public class Result
    {
        [JsonProperty("Data")]
        public GenericResultModelResponse Data { get; set; }


        [JsonProperty("AuthToken")]
        public AuthTokenResponse AuthToken { get; set; }
    }
}
