using Business.Dominio;
using Business.Services.Interfaces;
using Storage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CabeceraInteraccionLocalService : ICabeceraInteraccionLocalService
    {
        #region Services

        private readonly IRepository<CabeceraInteraccionLocal> repository;

        #endregion

        #region Constructors

        public CabeceraInteraccionLocalService(IRepository<CabeceraInteraccionLocal> repository)
        {
            this.repository = repository;
        }

        #endregion

        public Task Delete()
        {
            return repository.Delete();
        }
        public Task Delete(CabeceraInteraccionLocal cabeceraInteraccion)
        {
            return repository.Delete(cabeceraInteraccion);
        }

        public Task DeleteAllByIdsAsync(IEnumerable<object> primaryKeys)
        {
            return repository.DeleteAllByIdsAsync(primaryKeys);
        }

        public Task Format()
        {
            return repository.Format();
        }

        public Task<List<CabeceraInteraccionLocal>> GetAllDB()
        {
           return repository.GetAll();
        }

        public Task Save(CabeceraInteraccionLocal cabeceraInteraccion)
        {
            return repository.Save(cabeceraInteraccion);
        }
        public Task Save(List<CabeceraInteraccionLocal> cabecerasInteraccion)
        {
            return repository.SaveAll(cabecerasInteraccion);
        }
        public Task Insert(CabeceraInteraccionLocal cabeceraInteraccion)
        {
            return repository.Insert(cabeceraInteraccion);
        }
        public Task InsertAll(List<CabeceraInteraccionLocal> cabecerasInteraccion)
        {
            return repository.InsertAll(cabecerasInteraccion);
        }
        public Task Update(CabeceraInteraccionLocal cabeceraInteraccion) {
            return repository.Update(cabeceraInteraccion);
        }
        public Task UpdateAll(List<CabeceraInteraccionLocal> cabecerasInteraccion)
        {
            return repository.UpdateAll(cabecerasInteraccion);
        }
        public Task<List<CabeceraInteraccionLocal>> Query(string query, params object[] args)
        {
            return repository.Query(query, args);
        }
    }
}
