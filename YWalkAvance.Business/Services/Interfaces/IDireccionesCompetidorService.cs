using Business.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IDireccionesCompetidorService
    {

        Task Save(DireccionCompetidor direccionCompetidor);
        Task SaveAll(List<DireccionCompetidor> direccionCompetidores);
        Task Update(DireccionCompetidor direccionCompetidor);
        Task UpdateAll(List<DireccionCompetidor> direccionCompetidores);
        Task<DireccionCompetidor> GetWithChildrenDBAsync();

        Task<List<DireccionCompetidor>> GetByInterComercial(string interComercial);

        Task<DireccionCompetidor> GetDB();
        Task<List<DireccionCompetidor>> GetAllDB();

        Task Delete();
        Task Delete(string ID);

        Task Format();
    }
}
