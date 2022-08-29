using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Formulario5
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("FORMULARIO")]
        public string Formulario;
        [JsonProperty("PREGUNTA")]
        public string Pregunta;
        [JsonProperty("RESPUESTA")]
        public string Respuesta;
    }
}
