using Newtonsoft.Json;
using Services.Commons.PrecioProductos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_PreciosProductos
    {
        [JsonProperty("cabecera")]
        public IList<Cabecera> cabecera { get; set; }
        [JsonProperty("detalleProducto")]
        public IList<DetalleProducto> detalleProducto { get; set; }
        [JsonProperty("formulario1")]
        public IList<Formulario1> formulario1 { get; set; }
        [JsonProperty("formulario2")]
        public IList<Formulario2> formulario2 { get; set; }
        [JsonProperty("formulario2Trs")]
        public IList<Formulario2Trs> formulario2trs { get; set; }
        [JsonProperty("formulario3")]
        public IList<Formulario3> formulario3 { get; set; }
        [JsonProperty("formulario4")]
        public IList<Formulario4> formulario4 { get; set; }
        [JsonProperty("formulario5")]
        public IList<Formulario5> formulario5 { get; set; }
        [JsonProperty("interlocutores")]
        public IList<Interlocutores> interlocutores { get; set; }
        [JsonProperty("preciosProductos")]
        public IList<PreciosProductos> preciosProductos { get; set; }
        [JsonProperty("preciosSpot")]
        public IList<PreciosSpot> preciosSpot { get; set; }
        [JsonProperty("proveedor")]
        public IList<Proveedor> proveedor { get; set; }
        [JsonProperty("relaciones")]
        public IList<Relaciones> relaciones { get; set; }
        [JsonProperty("sac")]
        public IList<SAC> sac { get; set; }
    }
}
