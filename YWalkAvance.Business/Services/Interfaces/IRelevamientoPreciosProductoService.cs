using Business.Dominio;
using Services.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IRelevamientoPreciosProductoService
    {
        Task<List<RelevamientoPreciosProducto>> GetRelevamientoPreciosProducto();
        List<RelevamientoPreciosProducto> GetRelevamientoPreciosProductoPorBandera(int IdBandera);
        Task<LlamadaRFC_PreciosProductos> GetPreciosProductosSAP(string idRRCC, string codigoCompetidor);
        Task<List<GenericResultModelResponse>> GetPreciosProductosSAPAlternativo(List<CompetidorDTO> competidores);
        Task Save(List<RelevamientoPreciosProducto> relevamientoPreciosProductos);
        Task Save(RelevamientoPreciosProducto relevamientoPreciosProducto);
        Task<RelevamientoPreciosProducto> GetWithChildrenDBAsync();
        Task Update(RelevamientoPreciosProducto relevamientoPreciosProducto);
        Task UpdateAll(List<RelevamientoPreciosProducto> relevamientoPreciosProductos);
        Task<RelevamientoPreciosProducto> GetDB();
        Task<List<RelevamientoPreciosProducto>> GetAllDB();
        Task<List<RelevamientoPreciosProducto>> Query(string querySintax, params object[] args);
        Task Delete();
        Task Delete(RelevamientoPreciosProducto relevamientoPreciosProducto);
        Task Format();
    }
}
