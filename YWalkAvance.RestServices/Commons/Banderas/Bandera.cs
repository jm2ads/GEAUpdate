using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Banderas
{
    [JsonObject]
    public class Bandera
    {
        [JsonProperty("IdBandera")]
        public int IdBandera { get; set; }
        [JsonProperty("CodigoSAP")]
        public string CodigoSAP { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
    }
}
