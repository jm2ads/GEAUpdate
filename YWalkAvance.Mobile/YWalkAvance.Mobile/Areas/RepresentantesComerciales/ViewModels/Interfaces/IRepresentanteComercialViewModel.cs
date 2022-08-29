using Frontend.Mobile.Commons.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Mobile.Areas.RepresentantesComerciales.ViewModels.Interfaces
{
    public interface IRepresentanteComercialViewModel
    {
        //Task<RepresentanteComercialGroupModel> GetRepresentanteComercial(string idRRCC);

        string GetBuildVersion();
    }
}
