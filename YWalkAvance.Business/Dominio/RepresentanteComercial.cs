using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("RepresentanteComercial")]
    [JsonObject]
    public class RepresentanteComercial : SyncEntity
    {

        [PrimaryKey]
        [JsonProperty("CodigoInterlocutor")]
        public string CodigoInterlocutor { get; set; }
        [JsonProperty("Usuario")]
        public string Usuario { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Apellido")]
        public string Apellido { get; set; }

        [JsonProperty("IdNegocio")]
        public string IdNegocio { get; set; }
    }
}
