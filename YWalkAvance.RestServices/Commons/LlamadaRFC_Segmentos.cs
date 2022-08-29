using Newtonsoft.Json;
using Services.Commons.Negocios;
using Services.Commons.Segmentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_Segmentos
    {
        [JsonProperty("segmentos")]
        public IList<Segmento> Segmentos { get; set; }
    }
}
