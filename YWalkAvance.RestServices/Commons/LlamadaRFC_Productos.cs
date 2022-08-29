using Newtonsoft.Json;
using Services.Commons.Productos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_Productos
    {
        [JsonProperty("productos")]
        public IList<Producto> Productos { get; set; }
    }
}
