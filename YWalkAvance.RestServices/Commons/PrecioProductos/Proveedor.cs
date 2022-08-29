using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Proveedor
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("OBJECT_ID")]
        public string ObjectId;
        [JsonProperty("NRO_LINEA")]
        public string NroLinea;
        [JsonProperty("MARCA")]
        public string Marca;
        [JsonProperty("CANAL")]
        public string Canal;
        [JsonProperty("DESPACHO")]
        public string Despacho;
        [JsonProperty("LOGISTICA")]
        public string Logistica;
    }
}
