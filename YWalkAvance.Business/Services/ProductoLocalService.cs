using Business.Dominio;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Services.Commons;
using Storage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductoLocalService : IProductoLocalService
    {
        #region Services

        private readonly IRepository<ProductoLocal> repository;

        #endregion

        #region Constructors

        public ProductoLocalService(IRepository<ProductoLocal> repository)
        {
            this.repository = repository;
        }

        #endregion

        public Task Delete()
        {
            return repository.Delete();
        }

        public Task DeleteProduct(ProductoLocal entity) {
            return repository.Delete(entity);
        }

        public Task DeleteAllByIdsAsync(IEnumerable<object> primaryKeys)
        {
            return repository.DeleteAllByIdsAsync(primaryKeys);
        }

        public Task Format()
        {
            return repository.Format();
        }

        public Task<List<ProductoLocal>> GetAllDB()
        {
            return repository.GetAll();
        }

        public Task<List<ProductoLocal>> Query(string querySintax, params object[] args)
        {
            return repository.Query(querySintax, args);
        }

        public Task SaveAll(List<ProductoLocal> productosLocal)
        {
            return repository.SaveAll(productosLocal);
        }
        public Task Save(ProductoLocal productoLocal)
        {
            return repository.Save(productoLocal);
        }
        public Task InsertAll(List<ProductoLocal> productosLocal)
        {
            return repository.InsertAll(productosLocal);
        }
        public Task Insert(ProductoLocal productoLocal)
        {
            return repository.Insert(productoLocal);
        }

        public Task UpdateAll(List<ProductoLocal> productosLocal)
        {
            return repository.UpdateAll(productosLocal);
        }
        public Task Update(ProductoLocal productoLocal)
        {
            return repository.Update(productoLocal);
        }

        public async Task<ResultArray> UploadToRFC(UploadRelevamientoPreciosModel relevamientoPreciosModel) {
            return await HttpClientService.PostToRFC<ResultArray>(ApiConstants.SetRelevamientoPrecios, relevamientoPreciosModel);
        }
    }
}
