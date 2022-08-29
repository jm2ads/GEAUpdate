using Business.Dominio;
using Business.Services.Interfaces;
using Storage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class DireccionesCompetidorService : IDireccionesCompetidorService
    {
        private readonly IRepository<DireccionCompetidor> repository;

        public DireccionesCompetidorService(IRepository<DireccionCompetidor> repository)
        {
            this.repository = repository;
        }

        public Task Delete()
        {
            return repository.Delete();
        }

        public Task Delete(string ID)
        {
            return repository.Delete(ID);
        }
        public Task Format()
        {
            return repository.Format();
        }
        public Task<List<DireccionCompetidor>> GetAllDB()
        {
            return repository.GetAll();
        }
        public Task<DireccionCompetidor> GetDB()
        {
            return repository.First();
        }

        public async Task<DireccionCompetidor> GetWithChildrenDBAsync()
        {
            var bandera = await repository.GetAllWithChildren();
            return bandera.First();
        }
        public async Task<List<DireccionCompetidor>> GetByInterComercial(string interComercial) {
            return await repository.Where(x => x.InterComercial == interComercial);
        }

        public Task Save(DireccionCompetidor direccionCompetidor)
        {
            return repository.Save(direccionCompetidor);
        }
        public Task SaveAll(List<DireccionCompetidor> direccionCompetidores)
        {
            return repository.SaveAll(direccionCompetidores);
        }
        public Task UpdateAll(List<DireccionCompetidor> direccionCompetidores)
        {
            return repository.UpdateAll(direccionCompetidores);
        }
        public Task Update(DireccionCompetidor direccionCompetidor)
        {
            return repository.Update(direccionCompetidor);
        }
    }
}
