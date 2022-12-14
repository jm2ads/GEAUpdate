using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.PrecioProductos
{
    [JsonObject]
    public class PreciosSpot
    {
        [JsonProperty("ID")]
        public string Id;
        [JsonProperty("PROD_ID")]
        public string ProdId;
        [JsonProperty("PROD_DESC")]
        public string ProdDesc;
        [JsonProperty("VOLUMEN")]
        public decimal Volumen;
        [JsonProperty("VOLUMENSpecified")]
        public bool VolumenSpecified;
        [JsonProperty("PREC_FINAL")]
        public decimal PrecFinal;
        [JsonProperty("PREC_FINALSpecified")]
        public bool PrecFinalSpecified;
        [JsonProperty("PREC_NETO")]
        public decimal PrecNeto;
        [JsonProperty("PREC_NETOSpecified")]
        public bool PrecNetoSpecified;
        [JsonProperty("BANDERA")]
        public string Bandera;

        public string cANAL_COMPField;

        public string rAZON_SOCField;

        public string cOD_PROVField;

        public string lOCALIDADField;

        public string eVID_FACTField;

        public string cON_FINANField;
    }
}
