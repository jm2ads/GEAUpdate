using Business.Dominio;
using Services.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface ICompetidoresService
    {
        Task<LlamadaRFC_Competidores> GetCompetidores(string idRed);

        Task Save(Competidor competidor);
        Task Save(List<Competidor> competidores);

        Task<Competidor> GetWithChildrenDB();

        Task<Competidor> GetDB();

        Task<List<Competidor>> GetAllDB();
        Task UpdateAll(List<Competidor> competidores);
        Task Update(Competidor competidor);
        Task Format();
        Task Delete();
        Task Delete(Competidor competidor);
        Task Delete(string Id);
        Task<List<Competidor>> Query(string query, params object[] args);
        Task<Competidor> GetCompetitorByHeader(string cliente);
        Task GenerarSincronizacionesPendientes(List<CompetidorPendiente> competidoresPendientes);
    }
}
