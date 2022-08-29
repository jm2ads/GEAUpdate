using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    [JsonObject]
    public class CabeceraInteraccion
    {
        [JsonProperty("ID")]
        public string Id { get; set; }
        [JsonProperty("CALLE")]
        public string Calle { get; set; }
        [JsonProperty("CATEGORIA")]
        public string Categoria { get; set; }
        [JsonProperty("CIUDAD")]
        public string Ciudad { get; set; }
        [JsonProperty("CLIENTE")]
        public string Cliente { get; set; }
        [JsonProperty("COD_POSTAL")]
        public string CodPostal { get; set; }
        [JsonProperty("COD_FORMULARIO")]
        public string CodFormulario { get; set; }
        [JsonProperty("COD_TRAN_INT")]
        public string CodTranInt { get; set; }
        [JsonProperty("DESCRIPCION")]
        public string Descripcion { get; set; }
        [JsonProperty("ESTADO")]
        public string Estado { get; set; }
        [JsonProperty("FECHA_CREAC")]
        public string FechaCreac { get; set; }
        [JsonProperty("FECHA_FIN_P")]
        public string FechaFinP { get; set; }
        [JsonProperty("FECHA_FIN_R")]
        public string FechaFinR { get; set; }
        [JsonProperty("FECHA_INI_P")]
        public string FechaIniP { get; set; }
        [JsonProperty("FECHA_INI_R")]
        public string FechaIniR { get; set; }
        [JsonProperty("HORA_FIN_P")]
        public string HoraFinP { get; set; }
        [JsonProperty("HORA_FIN_R")]
        public string HoraFinR { get; set; }
        [JsonProperty("HORA_INI_P")]
        public string HoraIniP { get; set; }
        [JsonProperty("HORA_INI_R")]
        public string HoraIniR { get; set; }
        [JsonProperty("LATITUD")]
        public string Latitud { get; set; }
        [JsonProperty("LONGITUD")]
        public string Longitud { get; set; }
        [JsonProperty("MOTIVO")]
        public string Motivo { get; set; }
        [JsonProperty("NEGOCIO")]
        public string Negocio { get; set; }
        [JsonProperty("NOMBRE_CLIENTE")]
        public string NombreCliente { get; set; }
        [JsonProperty("NOMBRE_RESPONSABLE")]
        public string NombreResponsable { get; set; }
        [JsonProperty("NOMBRE_RRCC")]
        public string NombreRRCC { get; set; }
        [JsonProperty("NRO_ACTIVIDAD")]
        public string NroActividad { get; set; }
        [JsonProperty("NUMERO")]
        public string Numero { get; set; }
        [JsonProperty("OPERACION")]
        public string Operacion { get; set; }
        [JsonProperty("PAIS")]
        public string Pais { get; set; }
        [JsonProperty("PROVINCIA")]
        public string Provincia { get; set; }
        [JsonProperty("PUNTAJE")]
        public string Puntaje { get; set; }
        [JsonProperty("PUNTAJESpecified")]
        public bool PuntajeSpecified { get; set; }
        [JsonProperty("RESPONSABLE")]
        public string Responsable { get; set; }
        [JsonProperty("RRCC")]
        public string RRCC { get; set; }
        [JsonProperty("SEGMENTO")]
        public string Segmento { get; set; }
        [JsonProperty("TEXTO_0002")]
        public string Texto0002 { get; set; }
        [JsonProperty("TEXTO_ZR01")]
        public string TextoZR01 { get; set; }
        [JsonProperty("TEXTO_ZR02")]
        public string TextoZR02 { get; set; }
        [JsonProperty("TEXTO_ZR07")]
        public string TextoZR07 { get; set; }
        [JsonProperty("TEXTO_ZR08")]
        public string TextoZR08 { get; set; }
        [JsonProperty("TEXTO_ZR09")]
        public string TextoZR09 { get; set; }
        [JsonProperty("TEXTO_ZR10")]
        public string TextoZR10 { get; set; }
        [JsonProperty("TEXTO_ZR11")]
        public string TextoZR11 { get; set; }
    }
}
