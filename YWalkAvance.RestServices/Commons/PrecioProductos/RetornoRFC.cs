using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class RetornoRFC
    {
        [JsonProperty("TYPE")]
        public string TYPE { get; set; }
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("NUMBER")]
        public string NUMBER { get; set; }
        [JsonProperty("MESSAGE")]
        public string MESSAGE { get; set; }
        [JsonProperty("LOG_NO")]
        public string LOG_NO { get; set; }
        [JsonProperty("LOG_MSG_NO")]
        public string LOG_MSG_NO { get; set; }
        [JsonProperty("MESSAGE_V1")]
        public string MESSAGE_V1 { get; set; }
        [JsonProperty("MESSAGE_V2")]
        public string MESSAGE_V2 { get; set; }
        [JsonProperty("MESSAGE_V3")]
        public string MESSAGE_V3 { get; set; }
        [JsonProperty("MESSAGE_V4")]
        public string MESSAGE_V4 { get; set; }
        [JsonProperty("PARAMETER")]
        public string PARAMETER { get; set; }
        [JsonProperty("ROW")]
        public string ROW { get; set; }
        [JsonProperty("FIELD")]
        public string FIELD { get; set; }
        [JsonProperty("SYSTEM")]
        public string SYSTEM { get; set; }



    }
}
