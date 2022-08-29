using Newtonsoft.Json;
using Services.Commons.Competidores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class LlamadaRFC_Competidores
    {
        [JsonProperty("atributosMKT")]
        public IList<AtributosMKT> atributosMKT { get; set; }
        [JsonProperty("contratos")]
        public IList<Contratos> contratos { get; set; }
        [JsonProperty("datosGenerales")]
        public IList<DatosGenerales> datosGenerales { get; set; }
        [JsonProperty("direcciones")]
        public IList<Direcciones> direcciones { get; set; }
        [JsonProperty("interlocutor")]
        public IList<Interlocutor> interlocutor { get; set; }
        [JsonProperty("mails")]
        public IList<Mails> mails { get; set; }
        [JsonProperty("roles")]
        public IList<Roles> roles { get; set; }

    }
}
