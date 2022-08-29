using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Negocios
{
    [JsonObject]
    public class Negocio
    {
        [JsonProperty("IdNegocio")]
        public int IdNegocio { get; set; }
        [JsonProperty("IdSegmento")]
        public int IdSegmento { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("CodigoSAP")]
        public string CodigoSAP { get; set; }
    }
}
