using Newtonsoft.Json;
using System.Collections.Generic;

namespace Services.Commons
{
    [JsonObject]
    public class ResultArray
    {
        [JsonProperty("Data")]
        public List<GenericResultModelResponse> Data { get; set; }


        [JsonProperty("AuthToken")]
        public AuthTokenResponse AuthToken { get; set; }
    }
}
