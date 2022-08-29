using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Formulario1
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("INSPECCION")]
        public string Inspeccion;
        [JsonProperty("PRECIO")]
        public string Precio;
        [JsonProperty("CLUSTER")]
        public string Cluster;
        [JsonProperty("RED")]
        public string Red;
        [JsonProperty("APIES")]
        public string Apies;
        [JsonProperty("MERCADO")]
        public string Mercado;
        [JsonProperty("SUBMERCADO")]
        public string Submercado;
        [JsonProperty("PRECIO_TASA")]
        public decimal PrecioTasa;
        [JsonProperty("PRECIO_TASASpecified")]
        public bool PrecioTasaSpecified;
        [JsonProperty("OBSER_TASA")]
        public string ObserTasa;
        [JsonProperty("INCLUIDA")]
        public string Incluida;
        [JsonProperty("ESTRATEGICO")]
        public string Estrategico;
        [JsonProperty("CAMARA")]
        public string Camara;
        [JsonProperty("PROG_FIDELIZAC")]
        public string ProgFidelizac;
        [JsonProperty("OTROS_NEGOCIOS")]
        public string OtrosNegocios;
        [JsonProperty("MEDIOS_PAGO")]
        public string MediosPago;
    }
}
