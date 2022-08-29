using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Competidores
{
    [JsonObject]
    public class Mails
    {
        [JsonProperty("INTER_COMERCIAL")]
        public string InterComercial { get; set; }
        [JsonProperty("MAIL")]
        public string Mail { get; set; }
    }
}
