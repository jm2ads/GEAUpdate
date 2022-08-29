using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class SAC
    {
        [JsonProperty("ID_ACTIVIDAD")]
        public string IdActividad;
        [JsonProperty("NRO_REMITO")]
        public string NroRemito;
        [JsonProperty("SUB_MOTIVO")]
        public string SubMotivo;
        [JsonProperty("NRO_LICITACION")]
        public string NroLicitacion;
        [JsonProperty("GGRR")]
        public string Ggrr;
        [JsonProperty("DESC_GGRR")]
        public string DescGGRR;
        [JsonProperty("INC_CONTING")]
        public string IncConting;
        [JsonProperty("INC_TECNICO")]
        public string IncTecnico;
        [JsonProperty("CONTACTO")]
        public string Contacto;
        [JsonProperty("NRO_TARJETA")]
        public string NroTarjeta;
        [JsonProperty("NRO_SIR")]
        public string NroSir;
        [JsonProperty("REFERENTE")]
        public string Referente;
        [JsonProperty("COD_OPERACION")]
        public string CodOperacion;
        [JsonProperty("MOTIVO")]
        public string Motivo;
        [JsonProperty("DESC_SUB_MOTIVO")]
        public string DescSubMotivo;
    }
}
