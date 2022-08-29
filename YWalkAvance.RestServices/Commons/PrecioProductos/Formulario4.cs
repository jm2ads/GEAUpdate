using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Formulario4
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("TAMANO")]
        public string Tamano;
        [JsonProperty("SEGMENTO")]
        public string Segmento;
        [JsonProperty("SUBSEGMENTO")]
        public string Subsegmento;
        [JsonProperty("RAZ_SOC_1")]
        public string RazSoc1;
    }
}
