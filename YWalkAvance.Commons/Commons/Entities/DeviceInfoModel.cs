using Commons.Commons.Constants;
using Newtonsoft.Json;

namespace Commons.Commons.Entities
{
    public class DeviceInfoModel
    {
        public DeviceInfoModel()
        {
            this.application = ApplicationConstants.ApplicationName;
        }

        [JsonProperty("userlogin")]
        public string username { get; set; }

        [JsonProperty("userpass", NullValueHandling = NullValueHandling.Ignore)]
        public string password { get; set; }

        [JsonProperty("deviceid")]
        public DeviceInfo deviceid { get; set; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string token { get; set; }

        [JsonProperty("application")]
        public string application { get; set; }

        [JsonProperty("userimpersonate")]
        public string usernameimpersonate { get; set; }
    }
}
