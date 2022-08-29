using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("DireccionCompetidor")]
    [JsonObject]
    public class DireccionCompetidor : SyncEntity
    {
        [PrimaryKey]
        [JsonProperty("INTER_COMERCIAL")]
        public string InterComercial { get; set; }
        [JsonProperty("CALLE")]
        public string Calle { get; set; }
        [JsonProperty("NUMERO")]
        public string Numero { get; set; }
        [JsonProperty("COD_POSTAL")]
        public string CodPostal { get; set; }
        [JsonProperty("PROVINCIA")]
        public string Provincia { get; set; }
    }
}
