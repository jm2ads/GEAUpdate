using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Mobile.Areas.Sync.ViewModels.Interfaces
{
    public interface ISyncViewModel
    {
        Task<bool> GetData();
        Task CerrarPantalla();
        Task CerrarPantallasinPopUp();
        
        Task<bool> GuardarDB();
    }
}
