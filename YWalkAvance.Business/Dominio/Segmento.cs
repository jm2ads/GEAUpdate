using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("Segmento")]
    [JsonObject]
    public class Segmento : SyncEntity
    {
        [PrimaryKey]
        [JsonProperty("IdSegmento")]
        public int IdSegmento { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("CodigoSAP")]
        public string CodigoSAP { get; set; }
    }
}
