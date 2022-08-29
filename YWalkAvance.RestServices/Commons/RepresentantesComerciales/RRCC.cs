using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class RRCC
    {
        [JsonProperty("USUARIO")]
        public string Usuario { get; set; }

        [JsonProperty("COD_INT_SAP")]
        public string CodigoInterlocutor { get; set; }

        [JsonProperty("NOMBRE")]
        public string Nombre { get; set; }

        [JsonProperty("APELLIDO")]
        public string Apellido { get; set; }

        [JsonProperty("ID_NEGOCIO")]
        public string IdNegocio { get; set; }
    }
}