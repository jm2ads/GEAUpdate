using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("Provincia")]
    [JsonObject]
    public class Provincia : SyncEntity
    {
        [PrimaryKey]
        [JsonProperty("IdProvincia")]
        public int IdProvincia { get; set; }
        [JsonProperty("Nombre")]
        public string Nombre { get; set; }
        [JsonProperty("CodigoSAP")]
        public string CodigoSAP { get; set; }
        [JsonProperty("PaisSAP")]
        public string PaisSAP { get; set; }
    }
}
