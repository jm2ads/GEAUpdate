using Business.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface ICabeceraInteraccionLocalService
    {
        Task Save(CabeceraInteraccionLocal cabecerasInteraccion);
        Task Save(List<CabeceraInteraccionLocal> cabecerasInteraccion);
        Task Insert(CabeceraInteraccionLocal cabeceraInteraccion);
        Task InsertAll(List<CabeceraInteraccionLocal> cabecerasInteraccion);

        Task Update(CabeceraInteraccionLocal cabecerasInteraccion);
        Task UpdateAll(List<CabeceraInteraccionLocal> cabecerasInteraccion);
        Task DeleteAllByIdsAsync(IEnumerable<object> primaryKeys);
        Task Delete();
        Task Delete(CabeceraInteraccionLocal cabecerasInteraccion);

        Task<List<CabeceraInteraccionLocal>> GetAllDB();
        Task<List<CabeceraInteraccionLocal>> Query(string query, params object[] args);
        Task Format();
    }
}
