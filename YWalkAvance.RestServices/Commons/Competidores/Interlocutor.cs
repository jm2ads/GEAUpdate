using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Competidores
{
    [JsonObject]
    public class Interlocutor
    {
        [JsonProperty("NOM_USER_RRCC")]
        public string NomUserRRCC { get; set; }
    }
}
