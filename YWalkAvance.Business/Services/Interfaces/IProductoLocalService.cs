using Business.Dominio;
using Services.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IProductoLocalService
    {
        Task SaveAll(List<ProductoLocal> productosLocal);
        Task Save(ProductoLocal productoLocal);
        Task Insert(ProductoLocal productoLocal);
        Task InsertAll(List<ProductoLocal> productosLocal);
        Task UpdateAll(List<ProductoLocal> productosLocal);
        Task Update(ProductoLocal productoLocal);
        Task Delete();
        Task DeleteProduct(ProductoLocal entity);
        Task DeleteAllByIdsAsync(IEnumerable<object> primaryKeys);

        Task<List<ProductoLocal>> GetAllDB();
        Task<List<ProductoLocal>> Query(string querySintax, params object[] args);
        Task<ResultArray> UploadToRFC(UploadRelevamientoPreciosModel relevamientoPreciosModel);
        Task Format();
    }
}
