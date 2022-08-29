using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("Competidor")]
    [JsonObject]
    public class Competidor : SyncEntity
    {

        /*[JsonProperty("IdCompetidor")]
        [PrimaryKey, AutoIncrement]
        public int? IdCompetidor { get; set; }*/
        [JsonProperty("InterComercial")]
        public string InterComercial { get; set; }
        [PrimaryKey]
        [JsonProperty("APIES")]
        public string APIES { get; set; }
        [JsonProperty("RazonSocial")]
        public string RazonSocial { get; set; }
        [JsonProperty("Cuit")]
        public string Cuit { get; set; }
        [JsonProperty("Agrupacion")]
        public string Agrupacion { get; set; }
        [JsonProperty("UnidadNegocio")]
        public string UnidadNegocio { get; set; }
        [JsonProperty("Contacto")]
        public string Contacto { get; set; }
        [JsonProperty("CuentaSGC")]
        public string CuentaSGC { get; set; }
        [JsonProperty("CuentaLPO")]
        public string CuentaLPO { get; set; }
        [JsonProperty("CuentaLP2")]
        public string CuentaLP2 { get; set; }
        [JsonProperty("CuentaQP1")]
        public string CuentaQP1 { get; set; }
        [JsonProperty("NumeroExpediente")]
        public string NumeroExpediente { get; set; }
        [JsonProperty("OperaYer")]
        public string OperaYer { get; set; }
        [JsonProperty("CantTarjetaYer")]
        public string CantTarjetaYer { get; set; }
        [JsonProperty("CodDirent")]
        public string CodDirent { get; set; }
        [JsonProperty("Atributo")]
        public string Atributo { get; set; } //Atributo = Bandera
        [JsonProperty("Latitud")]
        public string Latitud { get; set; }
        [JsonProperty("Longitud")]
        public string Longitud { get; set; }
        [JsonProperty("Estado")]
        public CompetitorState Estado { get; set; }
    }
}
