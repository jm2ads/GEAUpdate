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
    public class RelevamientoPreciosProductoService : IRelevamientoPreciosProductoService
    {
        private readonly IRepository<RelevamientoPreciosProducto> repository;

        public RelevamientoPreciosProductoService(IRepository<RelevamientoPreciosProducto> repository)
        {
            this.repository = repository;
        }

        public Task Delete()
        {
            return repository.Delete();
        }

        public Task Delete(RelevamientoPreciosProducto relevamientoPreciosProducto)
        {
            return repository.Delete(relevamientoPreciosProducto);
        }

        public Task Format()
        {
            return repository.Format();
        }

        public Task<List<RelevamientoPreciosProducto>> GetAllDB()
        {
            return repository.GetAll();
        }
        public Task<RelevamientoPreciosProducto> GetDB()
        {
            return repository.First();
        }

        public async Task<LlamadaRFC_PreciosProductos> GetPreciosProductosSAP(string idRRCC, string codigoCompetidor)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>(){
                       new KeyValuePair<string, string>("idRRCC", idRRCC.ToString()),
                       new KeyValuePair<string, string>("codigoCompetidor", codigoCompetidor.ToString())
            };

            LlamadaRFC_PreciosProductos llamadaRFC_PreciosProductos = await HttpClientService.GetPreciosProductos<LlamadaRFC_PreciosProductos>(ApiConstants.SetPreciosProductos, values);

            return llamadaRFC_PreciosProductos;
        }



        public async Task<List<GenericResultModelResponse>> GetPreciosProductosSAPAlternativo(List<CompetidorDTO> competidores)
        {
            var resultados = await HttpClientService.PostContentReturn("rp/preciosProductosAlternativo", competidores);
            return resultados.Data;
        }

        public async Task<List<RelevamientoPreciosProducto>> GetRelevamientoPreciosProducto()
        {
            LlamadaRFC_Productos llamadaRFC_Productos = await HttpClientService.GetProductos<LlamadaRFC_Productos>(ApiConstants.GetProductos);

            List<RelevamientoPreciosProducto> relevamientoPreciosProductos = new List<RelevamientoPreciosProducto>();
            RelevamientoPreciosProducto producto = null;

            foreach (var item in llamadaRFC_Productos.Productos)
            {
                producto = new RelevamientoPreciosProducto() {
                    IdRelevamientoPreciosProducto = item.IdRelevamientoPreciosProducto,
                    CodigoSAP = item.CodigoSAP,
                    Descripcion = item.Descripcion,
                    IdSegmento = item.IdSegmento,
                    Envase = item.Envase,
                    Downloaded = DateTime.Now
                };
                relevamientoPreciosProductos.Add(producto);
            }

            return relevamientoPreciosProductos;
        }

        public List<RelevamientoPreciosProducto> GetRelevamientoPreciosProductoPorBandera(int IdBandera)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>(){
                       new KeyValuePair<string, string>("IdBandera", IdBandera.ToString())
            };

            LlamadaRFC_Productos llamadaRFC_Productos = HttpClientService.GetProductos<LlamadaRFC_Productos>(ApiConstants.GetProductos, values).Result;

            List<RelevamientoPreciosProducto> relevamientoPreciosProductos = new List<RelevamientoPreciosProducto>();
            RelevamientoPreciosProducto producto = null;

            foreach (var item in llamadaRFC_Productos.Productos)
            {
                producto = new RelevamientoPreciosProducto()
                {
                    IdRelevamientoPreciosProducto = item.IdRelevamientoPreciosProducto,
                    CodigoSAP = item.CodigoSAP,
                    Descripcion = item.Descripcion,
                    IdSegmento = item.IdSegmento,
                    Envase = item.Envase,
                    Downloaded = DateTime.Now
                };
                relevamientoPreciosProductos.Add(producto);
            }

            return relevamientoPreciosProductos;
        }

        public async Task<RelevamientoPreciosProducto> GetWithChildrenDBAsync()
        {
            var RRCC = await repository.GetAllWithChildren();
            return RRCC.First();
        }

        public Task<List<RelevamientoPreciosProducto>> Query(string querySintax, params object[] args)
        {
            return repository.Query(querySintax, args);
        }

        public Task Save(List<RelevamientoPreciosProducto> relevamientoPreciosProductos)
        {
            return repository.SaveAll(relevamientoPreciosProductos);
        }

        public Task Save(RelevamientoPreciosProducto relevamientoPreciosProducto)
        {
            return repository.Save(relevamientoPreciosProducto);
        }

        public Task UpdateAll(List<RelevamientoPreciosProducto> relevamientoPreciosProductos)
        {
            return repository.UpdateAll(relevamientoPreciosProductos);
        }

        public Task Update(RelevamientoPreciosProducto relevamientoPreciosProducto)
        {
            return repository.Update(relevamientoPreciosProducto);
        }
    }


    public class CompetidorDTO
    {
        public string IdRRCC { get; set; }
        public string CodigoCompetidor { get; set; }
    }

}
