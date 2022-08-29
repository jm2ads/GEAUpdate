using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Provincias
{
    [JsonObject]
    public class Provincia
    {
        [JsonProperty("IdProvincia")]
        public int IdProvincia { get; set; }
        [JsonProperty("Nombre")]
        public string Nombre { get; set; }
        [JsonProperty("CodigoSAP")]
        public string CodigoSAP { get; set; }
        [JsonProperty("PaisSAP")]
        public string PaisSAP { get; set; }
    }
}
