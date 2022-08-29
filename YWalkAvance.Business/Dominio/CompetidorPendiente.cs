using System;
using System.Collections.Generic;

namespace Business.Dominio
{
    public class CompetidorPendiente
    {
        public string RepresentanteComercial { get; set; }
        public string APIES { get; set; }
        public string RazonSocial { get; set; }
        public DateTime FechaRelevamiento { get; set; }
        public string Bandera { get; set; }
        public string Direccion { get; set; }
        public string Provincia { get; set; }
        public List<ProductoPendiente> ProductosPendientes { get; set; }
    }
}
