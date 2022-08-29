using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Competidores
{
    [JsonObject]
    public class Telefonos
    {
        [JsonProperty("INTER_COMERCIAL")]
        public string InterComercial { get; set; }
        [JsonProperty("TELEFONO")]
        public string Telefono { get; set; }
    }
}
