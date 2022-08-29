using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("Bandera")]
    [JsonObject]
    public class Bandera : SyncEntity
    {
        [PrimaryKey]
        [JsonProperty("IdBandera")]
        public int IdBandera { get; set; }
        [JsonProperty("CodigoSAP")]
        public string CodigoSAP { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
    }
}
