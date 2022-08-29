using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons
{
    public class UploadRelevamientoPreciosModel
    {
        public string Owner { get; set; }
        public UploadResultsTipoForRfcModel[] InteraccionKeys { get; set; }
        public List<CabeceraInteraccion> Interacciones { get; set; }
        public List<RelevamientoPreciosProductoUpload> RelevamientoPrecios { get; set; }
    }
}
