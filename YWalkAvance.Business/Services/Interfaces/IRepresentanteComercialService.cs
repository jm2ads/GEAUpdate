using Business.Dominio;
using Services.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IRepresentanteComercialService
    {
        Task<IList<RRCC>> GetRepresentanteComercial(string idRRCC);

        Task Save(RepresentanteComercial RRCC);
        Task Update(RepresentanteComercial RRCC);
        Task<RepresentanteComercial> GetWithChildrenDBAsync();

        Task<RepresentanteComercial> GetDB();
        Task<RepresentanteComercial> GetOneFromDB(string codigoInterlocutor);

        Task Delete();
        Task Format();
    }
}
