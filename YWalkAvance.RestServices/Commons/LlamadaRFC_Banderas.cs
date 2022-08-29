using Newtonsoft.Json;
using Services.Commons.Banderas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_Banderas
    {
        [JsonProperty("banderas")]
        public IList<Bandera> Banderas { get; set; }
    }
}
