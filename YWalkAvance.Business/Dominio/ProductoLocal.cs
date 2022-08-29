using Commons.Commons.Entities;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dominio
{
    [Table("ProductoLocal")]
    [JsonObject]
    public class ProductoLocal : SyncEntity
    {
        [JsonProperty("ID_PRODUCTO_LOCAL")]
        [PrimaryKey, AutoIncrement]
        public int? IdProductoLocal { get; set; }
        [JsonProperty("ENVASE")]
        public string Envase { get; set; }
        [JsonProperty("ID_CABECERA")]
        public string IdCabecera { get; set; }
        [ForeignKey(typeof(CabeceraInteraccionLocal))]
        [JsonProperty("CLIENTE")]
        public string Cliente { get; set; }
        [JsonProperty("PRECIO")]
        public string Precio { get; set; }
        [JsonProperty("PRECIO_SPECIFIED")]
        public bool PrecioSpecified { get; set; }
        [JsonProperty("PRECIO_COMPRA")]
        public string PrecioCompra { get; set; }
        [JsonProperty("PRECIO_COMPRASpecified")]
        public bool PrecioCompraSpecified { get; set; }
        [JsonProperty("PRECIO_DIST")]
        public string PrecioDist { get; set; }
        [JsonProperty("PRECIO_DISTSpecified")]
        public bool PrecioDistSpecified { get; set; }
        [JsonProperty("PRODUCTO")]
        public string Producto { get; set; }
        [JsonProperty("VOLUMEN")]
        public string Volumen { get; set; }
        [JsonProperty("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
    }
}
