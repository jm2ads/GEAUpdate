using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Productos
{
    [JsonObject]
    public class Producto
    {
        [JsonProperty("IdRelevamientoPreciosProducto")]
        public int IdRelevamientoPreciosProducto { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("CodigoSAP")]
        public string CodigoSAP { get; set; }
        [JsonProperty("IdSegmento")]
        public int IdSegmento { get; set; }
        [JsonProperty("Envase")]
        public string Envase { get; set; }
    }
}
