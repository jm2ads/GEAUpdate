using Newtonsoft.Json;
using Services.Commons.Provincias;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_Provincias
    {
        [JsonProperty("provincias")]
        public IList<Provincia> Provincias { get; set; }
    }
}
