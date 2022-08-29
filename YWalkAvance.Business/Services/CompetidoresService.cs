using Business.Dominio;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Services.Commons;
using Storage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CompetidoresService : ICompetidoresService
    {
        private readonly IRepository<Competidor> repository;

        public CompetidoresService(IRepository<Competidor> repository)
        {
            this.repository = repository;
        }

        public Task Delete()
        {
            return repository.Delete();
        }

        public Task Delete(Competidor competidor)
        {
            return repository.Delete(competidor);
        }
        public Task Delete(string Id)
        {
            return repository.Delete(Id);
        }

        public async Task<LlamadaRFC_Competidores> GetCompetidores(string idRed)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>(){
                       new KeyValuePair<string, string>("idRed", idRed.ToString())
            };

            LlamadaRFC_Competidores llamadaRFC_Competidores = await HttpClientService.GetCompetidores<LlamadaRFC_Competidores>(ApiConstants.GetCompetidores, values);

            return llamadaRFC_Competidores;
        }

        public async Task GenerarSincronizacionesPendientes(List<CompetidorPendiente> competidoresPendientes)
        {
            await HttpClientService.PostNoContent(ApiConstants.GenerarSincronizacionesPendientes, competidoresPendientes);
        }

        public Task<List<Competidor>> GetAllDB() {
            return repository.GetAll();
        }

        public Task<Competidor> GetDB()
        {
            return repository.First();
        }

        public async Task<Competidor> GetCompetitorByHeader(string cliente)
        {
            var competidores = await repository.Query("SELECT * FROM Competidor WHERE InterComercial = ?", cliente);
            return competidores.FirstOrDefault();
        }

        public async Task<Competidor> GetWithChildrenDB()
        {
            var competidores = await repository.GetAllWithChildren();
            return competidores.First();
        }
        public Task UpdateAll(List<Competidor> competidores)
        {
            return repository.UpdateAll(competidores);
        }
        public Task Update(Competidor competidor)
        {
            return repository.Update(competidor);
        }

        public Task Save(Competidor competidor)
        {
            return repository.Save(competidor);
        }

        public Task Save(List<Competidor> competidores)
        {
            return repository.SaveAll(competidores);
        }

        public Task Format()
        {
            return repository.Format();
        }

        public Task<List<Competidor>> Query(string query, params object[] args) {
            return repository.Query(query, args);
        }
        
    }
}
