using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class GenericResultModelResponse
    {
        [JsonProperty("AmbienteSap")]
        public string AmbienteSap { get; set; }
        [JsonProperty("Propietario")]
        public string Propietario { get; set; }
        [JsonProperty("CodigoInterno")]
        public string CodigoInterno { get; set; }
        [JsonProperty("Formulario")]
        public string Formulario { get; set; }
        [JsonProperty("Motivo")]
        public string Motivo { get; set; }
        [JsonProperty("Tipo")]
        public string Tipo { get; set; }
        [JsonProperty("SubidaOk")]
        public bool SubidaOk { get; set; }
        [JsonProperty("ConsultaOk")]
        public bool ConsultaOk { get; set; }
        [JsonProperty("RespuestaSap")]
        public string RespuestaSap { get; set; }
        [JsonProperty("FechaCarga")]
        public DateTime FechaCarga { get; set; }
        [JsonProperty("LlamadaRfc")]
        public string LlamadaRfc { get; set; }
    }
}
