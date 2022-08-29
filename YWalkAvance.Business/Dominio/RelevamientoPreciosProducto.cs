using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("RelevamientoPreciosProducto")]
    [JsonObject]
    public class RelevamientoPreciosProducto : SyncEntity
    {
        [PrimaryKey]
        [JsonProperty("IdRelevamientoPreciosProducto")]
        public int IdRelevamientoPreciosProducto { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("CodigoSAP")]
        public string CodigoSAP { get; set; }
        [JsonProperty("IdSegmento")]
        public int IdSegmento { get; set; }
        [JsonProperty("Envase")]
        public string Envase { get; set; }
    }
}
