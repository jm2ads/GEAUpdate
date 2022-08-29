using Newtonsoft.Json;

namespace Services.Commons
{
    public class AuthToken
    {
        private string TokenField;
        private UserInfo UserInfoField;
        [JsonProperty("Token")]
        public string Token
        {
            get { return this.TokenField; }
            set { this.TokenField = value; }
        }
        [JsonProperty("UserInfo")]
        public UserInfo UserInfo
        {
            get { return this.UserInfoField; }
            set { this.UserInfoField = value; }
        }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("errType")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public string Success { get; set; }
    }
}
