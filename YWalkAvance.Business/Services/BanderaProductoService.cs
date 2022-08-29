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
    public class BanderaProductoService : IBanderaProductoService
    {
        private readonly IRepository<BanderaProducto> repository;

        public BanderaProductoService(IRepository<BanderaProducto> repository)
        {
            this.repository = repository;
        }

        public Task Delete()
        {
            return repository.Delete();
        }
        public Task Delete(BanderaProducto banderaProducto)
        {
            return repository.Delete(banderaProducto);
        }

        public Task Format()
        {
            return repository.Format();
        }

        public async Task<List<BanderaProducto>> GetBanderasProducto()
        {
            LlamadaRFC_BanderaProducto llamadaRFC_Banderas = await HttpClientService.GetBanderasProducto<LlamadaRFC_BanderaProducto>(ApiConstants.GetBanderasProducto);
            List<BanderaProducto> banderasProducto = new List<BanderaProducto>();
            BanderaProducto banderaProducto = null;
            foreach (var item in llamadaRFC_Banderas.BanderasProducto)
            {
                banderaProducto = new BanderaProducto()
                {
                    IdBandera = item.IdBandera,
                    IdBanderaProducto = item.IdBanderaProducto,
                    IdRelevamientoPreciosProducto = item.IdRelevamientoPreciosProducto,
                    Downloaded = DateTime.Now
                };
                banderasProducto.Add(banderaProducto);
            }

            return banderasProducto;
        }
        public Task<List<BanderaProducto>> GetAllDB()
        {
            return repository.GetAll();
        }
        public Task<BanderaProducto> GetDB()
        {
            return repository.First();
        }

        public async Task<BanderaProducto> GetWithChildrenDBAsync()
        {
            var bandera = await repository.GetAllWithChildren();
            return bandera.First();
        }
        public async Task<BanderaProducto> GetByIdDB(int idBandera)
        {
            return await repository.GetById(idBandera);
        }
        public Task Save(BanderaProducto banderaProducto)
        {
            return repository.Save(banderaProducto);
        }
        public Task Save(List<BanderaProducto> banderas)
        {
            return repository.SaveAll(banderas);
        }
        public Task UpdateAll(List<BanderaProducto> banderas)
        {
            return repository.UpdateAll(banderas);
        }
        public Task Update(BanderaProducto bandera)
        {
            return repository.Update(bandera);
        }
    }
}
