using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Relaciones
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("NRO_ACT_RELACIONADA")]
        public string NroActRelacionada;
    }
}
