using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Formulario2Trs
    {
        [JsonProperty("ID")]
        public string ID;
        [JsonProperty("RAZ_SOC_2")]
        public string RAZ_SOC_2;
        [JsonProperty("TEXTO_ZR03")]
        public string TEXTO_ZR03;
        [JsonProperty("TEXTO_ZR04")]
        public string TEXTO_ZR04;
        [JsonProperty("TEXTO_ZR05")]
        public string TEXTO_ZR05;
        [JsonProperty("TEXTO_ZR06")]
        public string TEXTO_ZR06;
    }
}
