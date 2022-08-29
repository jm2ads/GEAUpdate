using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Formulario2
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("NRO_ACTIVIDAD")]
        public string NroActividad;
        [JsonProperty("COMPETENCIA")]
        public string Competencia;
        [JsonProperty("PRODUCTO")]
        public string Producto;
        [JsonProperty("VOL_TOTAL")]
        public string VolTotal;
        [JsonProperty("VOL_DIR_ENTREGA")]
        public string VolDirEntrega;
    }
}
