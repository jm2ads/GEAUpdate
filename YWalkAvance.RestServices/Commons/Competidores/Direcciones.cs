using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Competidores
{
    [JsonObject]
    public class Direcciones
    {
        [JsonProperty("INTER_COMERCIAL")]
        public string InterComercial { get; set; }
        [JsonProperty("CALLE")]
        public string Calle { get; set; }
        [JsonProperty("NUMERO")]
        public string Numero { get; set; }
        [JsonProperty("COD_POSTAL")]
        public string CodPostal { get; set; }
        [JsonProperty("PROVINCIA")]
        public string Provincia { get; set; }
    }
}
