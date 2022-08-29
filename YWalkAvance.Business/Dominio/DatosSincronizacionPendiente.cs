using System;
using System.Collections.Generic;

namespace Business.Dominio
{
    public class DatosSincronizacionPendiente
    {

        public DateTime FechaRelevamiento { get; set; }
        public List<ProductoPendiente> ProductosPendientes { get; set; }

    }
}
