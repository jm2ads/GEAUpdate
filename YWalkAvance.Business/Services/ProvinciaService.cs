using Business.Dominio;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Services.Commons;
using Storage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly IRepository<Provincia> repository;

        public ProvinciaService(IRepository<Provincia> repository)
        {
            this.repository = repository;
        }

        public Task Delete()
        {
            return repository.Delete();
        }

        public Task Format()
        {
            return repository.Format();
        }

        public async Task<List<Provincia>> GetProvincias()
        {
            LlamadaRFC_Provincias llamadaRFC_Provincias = await HttpClientService.GetProvincias<LlamadaRFC_Provincias>(ApiConstants.GetProvincias);
            List<Provincia> provincias = new List<Provincia>();
            Provincia provincia = null;
            foreach (var item in llamadaRFC_Provincias.Provincias)
            {
                provincia = new Provincia()
                {
                    IdProvincia = item.IdProvincia,
                    Nombre = item.Nombre,
                    CodigoSAP = item.CodigoSAP,
                    PaisSAP = item.PaisSAP,
                    Downloaded = DateTime.Now
                };
                provincias.Add(provincia);
            }

            return provincias;
        }
        public Task<List<Provincia>> GetAllDB()
        {
            return repository.GetAll();
        }
        public Task<Provincia> GetDB()
        {
            return repository.First();
        }

        public async Task<Provincia> GetWithChildrenDBAsync()
        {
            var bandera = await repository.GetAllWithChildren();
            return bandera.First();
        }
        public async Task<Provincia> GetByIdDB(int idBandera)
        {
            return await repository.GetById(idBandera);
        }

        public Task Save(List<Provincia> provincias)
        {
            return repository.SaveAll(provincias);
        }
        public Task UpdateAll(List<Provincia> provincias)
        {
            return repository.UpdateAll(provincias);
        }
        public Task Update(Provincia provincia)
        {
            return repository.Update(provincia);
        }

        public Task<List<Provincia>> Query(string querySintax, params object[] args)
        {
            return repository.Query(querySintax, args);
        }
    }
}
