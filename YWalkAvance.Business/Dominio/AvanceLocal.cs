using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;

namespace Business.Dominio
{
    [Table("AvanceLocal")]
    [JsonObject]
    public class AvanceLocal : SyncEntity
    {

        [PrimaryKey, AutoIncrement]
        public int? ID { get; set; }

        [JsonProperty("TareaID")]
        public int TareaID { get; set; }

        [JsonProperty("PlanoID")]
        public int PlanoID { get; set; }

        [JsonProperty("PartidaID")]
        public int PartidaID { get; set; }

        [JsonProperty("CantidadAcumulada")]
        public double CantidadAcumulada { get; set; }

        [JsonProperty("Fecha")]
        public string Fecha { get; set; }

        [JsonProperty("UsuarioLogin")]
        public string UsuarioLogin { get; set; }
    }
}
