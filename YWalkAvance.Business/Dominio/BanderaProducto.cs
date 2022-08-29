using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("BanderaProducto")]
    [JsonObject]
    public class BanderaProducto : SyncEntity
    {
        [PrimaryKey]
        [JsonProperty("IdBanderaProducto")]
        public int IdBanderaProducto { get; set; }
        
        [JsonProperty("IdBandera")]
        [ForeignKey(typeof(Bandera))]
        public int IdBandera { get; set; }
       
        [JsonProperty("IdRelevamientoPreciosProducto")]
        [ForeignKey(typeof(RelevamientoPreciosProducto))]
        public int IdRelevamientoPreciosProducto { get; set; }
    }
}
