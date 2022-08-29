using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Competidores
{
    [JsonObject]
    public class AtributosMKT
    {
        [JsonProperty("INTER_COM")]
        public string InterCom { get; set; }
        [JsonProperty("MKT_SET")]
        public string MktSet { get; set; }
        [JsonProperty("ATTRIBUTO")]
        public string Attributo { get; set; }
        [JsonProperty("VALOR")]
        public string Valor { get; set; }
        [JsonProperty("VALOR_N")]
        public string ValorN { get; set; }
    }
}
