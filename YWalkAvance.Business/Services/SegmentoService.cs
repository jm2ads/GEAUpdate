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
    public class SegmentoService : ISegmentoService
    {
        private readonly IRepository<Segmento> repository;

        public SegmentoService(IRepository<Segmento> repository)
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

        public async Task<List<Segmento>> GetSegmentos()
        {
            LlamadaRFC_Segmentos llamadaRFC_Segmentos = await HttpClientService.GetSegmentos<LlamadaRFC_Segmentos>(ApiConstants.GetSegmentos);
            List<Segmento> Segmentos = new List<Segmento>();
            Segmento Segmento = null;
            foreach (var item in llamadaRFC_Segmentos.Segmentos)
            {
                Segmento = new Segmento()
                {
                    IdSegmento = item.IdSegmento,
                    CodigoSAP = item.CodigoSAP,
                    Descripcion = item.Descripcion,
                    Downloaded = DateTime.Now
                };
                Segmentos.Add(Segmento);
            }

            return Segmentos;
        }
        public Task<List<Segmento>> GetAllDB()
        {
            return repository.GetAll();
        }
        public Task<Segmento> GetDB()
        {
            return repository.First();
        }

        public async Task<Segmento> GetWithChildrenDBAsync()
        {
            var bandera = await repository.GetAllWithChildren();
            return bandera.First();
        }
        public async Task<Segmento> GetByIdDB(int IdNegocio)
        {
            return await repository.GetById(IdNegocio);
        }

        public Task Save(List<Segmento> negocios)
        {
            return repository.SaveAll(negocios);
        }
        public Task Update(Segmento segmento)
        {
            return repository.Save(segmento);
        }
        public Task UpdateAll(List<Segmento> segmentos)
        {
            return repository.SaveAll(segmentos);
        }
    }
}
