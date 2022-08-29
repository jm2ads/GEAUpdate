using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Formulario3
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("FORMULARIO")]
        public string Formulario;
        [JsonProperty("PREGUNTA")]
        public string Pregunta;
        [JsonProperty("RESPUESTA")]
        public string Respuesta;
        [JsonProperty("OBSERVACION")]
        public string Observacion;
        [JsonProperty("PUNTAJE")]
        public decimal Puntaje;
        [JsonProperty("PUNTAJESpecified")]
        public bool PuntajeSpecified;
    }
}
