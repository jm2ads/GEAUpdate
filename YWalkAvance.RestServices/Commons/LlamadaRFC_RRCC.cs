using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_RRCC
    {
        [JsonProperty("RRCC")]
        public IList<RRCC> RRCC { get; set; }

    }
}
