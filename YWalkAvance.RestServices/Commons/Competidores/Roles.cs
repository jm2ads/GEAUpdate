using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Competidores
{
    [JsonObject]
    public class Roles
    {
        [JsonProperty("INTER_COMERCIAL")]
        public string InterComercial { get; set; }
        [JsonProperty("ROL")]
        public string Rol { get; set; }
        [JsonProperty("DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}
