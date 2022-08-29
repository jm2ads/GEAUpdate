using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Banderas
{
    [JsonObject]
    public class BanderaProducto
    {
        [JsonProperty("IdBanderaProducto")]
        public int IdBanderaProducto { get; set; }
        [JsonProperty("IdBandera")]
        public int IdBandera { get; set; }
        [JsonProperty("IdRelevamientoPreciosProducto")]
        public int IdRelevamientoPreciosProducto { get; set; }
    }
}
