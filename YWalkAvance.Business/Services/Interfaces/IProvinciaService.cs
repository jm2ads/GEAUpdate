using Business.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IProvinciaService
    {
        Task<List<Provincia>> GetProvincias();

        Task Save(List<Provincia> provincia);
        Task Update(Provincia provincia);
        Task UpdateAll(List<Provincia> provincias);
        Task<Provincia> GetWithChildrenDBAsync();

        Task<Provincia> GetByIdDB(int idProvincia);
        Task<List<Provincia>> Query(string querySintax, params object[] args);

        Task<Provincia> GetDB();

        Task Delete();
        Task Format();
    }
}
