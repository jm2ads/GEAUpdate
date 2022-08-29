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
    public class NegocioService : INegocioService
    {
        private readonly IRepository<Negocio> repository;

        public NegocioService(IRepository<Negocio> repository)
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

        public async Task<List<Negocio>> GetNegocios()
        {
            LlamadaRFC_Negocios llamadaRFC_Negocios = await HttpClientService.GetNegocios<LlamadaRFC_Negocios>(ApiConstants.GetNegocios);
            List<Negocio> negocios = new List<Negocio>();
            Negocio negocio = null;
            foreach (var item in llamadaRFC_Negocios.Negocios)
            {
                negocio = new Negocio()
                {
                    IdNegocio = item.IdNegocio,
                    IdSegmento = item.IdSegmento,
                    CodigoSAP = item.CodigoSAP,
                    Descripcion = item.Descripcion,
                    Downloaded = DateTime.Now
                };
                negocios.Add(negocio);
            }

            return negocios;
        }
        public Task<List<Negocio>> GetAllDB()
        {
            return repository.GetAll();
        }
        public Task<Negocio> GetDB()
        {
            return repository.First();
        }

        public async Task<Negocio> GetWithChildrenDBAsync()
        {
            var bandera = await repository.GetAllWithChildren();
            return bandera.First();
        }
        public async Task<Negocio> GetByIdDB(int IdNegocio)
        {
            return await repository.GetById(IdNegocio);
        }

        public Task Save(List<Negocio> negocios)
        {
            return repository.SaveAll(negocios);
        }
        public Task Update(Negocio negocio)
        {
            return repository.Update(negocio);
        }

        public Task UpdateAll(List<Negocio> negocios)
        {
            return repository.UpdateAll(negocios);
        }
    }
}
