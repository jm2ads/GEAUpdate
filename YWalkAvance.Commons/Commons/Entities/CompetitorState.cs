using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Commons.Commons.Entities
{
    public enum CompetitorState
    {
        [Description("No Cargados")]
        NoPrizeSurvey = 0,
               
        [Description("Pendiente de Sincronizar")]
        PrizeNoSync, // se modifico en el dispositivo
        
        [Description("Sincronizados")]
        PrizeSync,

        [Description("Error al Sincronizar")]
        ErrorSync, // se modifico en el dispositivo

        [Description("Sincronización manual")]
        ManualSync
    }
}
