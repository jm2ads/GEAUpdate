using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class PreciosProductos
    {
        [JsonProperty("ID")]
        public string Id { get; set; }
        [JsonProperty("ENVASE")]
        public string Envase { get; set; }
        [JsonProperty("PRECIO")]
        public string Precio { get; set; }
        [JsonProperty("PRECIO_SPECIFIED")]
        public bool PrecioSpecified { get; set; }
        [JsonProperty("PRECIO_COMPRA")]
        public string PrecioCompra { get; set; }
        [JsonProperty("PRECIO_COMPRASpecified")]
        public bool PrecioCompraSpecified { get; set; }
        [JsonProperty("PRECIO_DIST")]
        public string PrecioDist { get; set; }
        [JsonProperty("PRECIO_DISTSpecified")]
        public bool PrecioDistSpecified { get; set; }
        [JsonProperty("PRODUCTO")]
        public string Producto { get; set; }
        [JsonProperty("VOLUMEN")]
        public string Volumen { get; set; }
    }
}
