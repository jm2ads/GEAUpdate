using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Competidores
{
    [JsonObject]
    public class DatosGenerales
    {
        [JsonProperty("INTER_COMERCIAL")]
        public string InterComercial { get; set; }
        [JsonProperty("APIES")]
        public string APIES { get; set; }
        [JsonProperty("RAZ_SOC")]
        public string RazSoc { get; set; }
        [JsonProperty("Estado")]
        public bool Estado { get; set; }
        [JsonProperty("CUIT")]
        public string CUIT { get; set; }
        [JsonProperty("AGRUPACION")]
        public string Agrupacion { get; set; }
        [JsonProperty("UNIDAD_NEGOCIO")]
        public string UnidadNegocio { get; set; }
        [JsonProperty("CONTACTO")]
        public string Contacto { get; set; }
        [JsonProperty("CUENTA_SGC")]
        public string CuentaSGC { get; set; }
        [JsonProperty("CUENTA_LPO")]
        public string CuentaLPO { get; set; }
        [JsonProperty("CUENTA_LP2")]
        public string CuentaLP2 { get; set; }
        [JsonProperty("CUENTA_QP1")]
        public string CuentaQP1 { get; set; }
        [JsonProperty("NUM_EXP")]
        public string NumExp { get; set; }
        [JsonProperty("OPERA_YER")]
        public string OperaYer { get; set; }
        [JsonProperty("CANT_TARJETA_YER")]
        public string CantTarjetaYer { get; set; }
        [JsonProperty("COD_DIRENT")]
        public string CodDirent { get; set; }
        [JsonProperty("ATRIBUTO")]
        public string Atributo { get; set; }
        [JsonProperty("LATITUD")]
        public string Latitud { get; set; }
        [JsonProperty("LONGITUD")]
        public string Longitud { get; set; }
        public List<Direcciones> Direcciones { get; set; }
    }
}
