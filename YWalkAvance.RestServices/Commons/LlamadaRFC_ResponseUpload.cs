using Newtonsoft.Json;
using Services.Commons.PrecioProductos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_ResponseUpload
    {
        [JsonProperty("cabecera")]
        public IList<Cabecera> cabecera { get; set; }
        [JsonProperty("proveedor")]
        public IList<Proveedor> proveedor { get; set; }
        [JsonProperty("detalleProducto")]
        public IList<DetalleProducto> detalleProducto { get; set; }
        [JsonProperty("formilario1")]
        public IList<Formilario1> formilario1 { get; set; }
        [JsonProperty("formulario2Mo")]
        public IList<Formulario2Mo> formulario2Mo { get; set; }
        [JsonProperty("zformulario2Trs")]
        public IList<ZFormulario2Trs> zformulario2trs { get; set; }
        [JsonProperty("formulario3")]
        public IList<Formulario3> formulario3 { get; set; }
        [JsonProperty("formulario4")]
        public IList<Formulario4> formulario4 { get; set; }
        [JsonProperty("formulario5")]
        public IList<Formulario5> formulario5 { get; set; }
        [JsonProperty("precioSpot")]
        public IList<PrecioSpot> precioSpot { get; set; }
        [JsonProperty("preciosProductos")]
        public IList<PreciosProductos> preciosProductos { get; set; }
        [JsonProperty("relaciones")]
        public IList<Relaciones> relaciones { get; set; }
        [JsonProperty("sac")]
        public IList<SAC> sac { get; set; }
        [JsonProperty("retornorfc")]
        public IList<RetornoRFC> retornorfc { get; set; }
    }
}
