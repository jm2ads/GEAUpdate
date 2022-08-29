using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class Interlocutores
    {
        [JsonProperty("COD_CLI")]
        public string CodCli;
        [JsonProperty("COD_RRCC")]
        public string CodRRCC;
    }
}
