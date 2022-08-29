using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class DetalleProducto
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("OBJECT_ID")]
        public string ObjectId;
        [JsonProperty("NOM_PROD")]
        public string NomProd;
        [JsonProperty("VOLUMEN")]
        public string Volumen;
        [JsonProperty("STOCK")]
        public string Stock;
        [JsonProperty("TIP_PRODUCTO")]
        public string TipProducto;
        [JsonProperty("CRITICIDAD")]
        public string Criticidad;
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
        [JsonProperty("DESCRIPCION")]
        public string Descripcion;
    }
}
