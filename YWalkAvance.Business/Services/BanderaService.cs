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
    public class BanderaService : IBanderaService
    {

        private readonly IRepository<Bandera> repository;

        public BanderaService(IRepository<Bandera> repository)
        {
            this.repository = repository;
        }

        public Task Delete()
        {
            return repository.Delete();
        }

        public Task Delete(Bandera bandera)
        {
            return repository.Delete(bandera);
        }

        public Task Format()
        {
            return repository.Format();
        }

        public async Task<List<Bandera>> GetBanderas()
        {
            LlamadaRFC_Banderas llamadaRFC_Banderas = await HttpClientService.GetBanderas<LlamadaRFC_Banderas>(ApiConstants.GetBanderas);
            List<Bandera> banderas = new List<Bandera>();
            Bandera bandera = null;
            foreach (var item in llamadaRFC_Banderas.Banderas)
            {
                bandera = new Bandera() {
                    IdBandera = item.IdBandera,
                    CodigoSAP = item.CodigoSAP,
                    Descripcion = item.Descripcion,
                    Downloaded = DateTime.Now
                };
                banderas.Add(bandera);
            }

            return banderas;
        }
        public Task<List<Bandera>> GetAllDB()
        {
            return repository.GetAll();
        }
        public Task<Bandera> GetDB()
        {
            return repository.First();
        }

        public async Task<Bandera> GetWithChildrenDBAsync()
        {
            var bandera = await repository.GetAllWithChildren();
            return bandera.First();
        }
        public async Task<Bandera> GetByIdDB(int idBandera)
        {
            return await repository.GetById(idBandera);
        }

        public Task Save(List<Bandera> banderas)
        {
            return repository.SaveAll(banderas);
        }
        public Task Save(Bandera bandera)
        {
            return repository.Save(bandera);
        }
        public Task UpdateAll(List<Bandera> banderas)
        {
            return repository.UpdateAll(banderas);
        }
        public Task Update(Bandera bandera)
        {
            return repository.Update(bandera);
        }
    }
}
