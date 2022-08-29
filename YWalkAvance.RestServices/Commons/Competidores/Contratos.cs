using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Competidores
{
    [JsonObject]
    public class Contratos
    {
        [JsonProperty("INTER_COMERCIAL")]
        public string InterComercial { get; set; }
        [JsonProperty("CONTRATO")]
        public string Contrato { get; set; }
    }
}
