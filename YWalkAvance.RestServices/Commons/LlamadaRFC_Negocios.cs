using Newtonsoft.Json;
using Services.Commons.Negocios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_Negocios
    {
        [JsonProperty("negocios")]
        public IList<Negocio> Negocios { get; set; }
    }
}
