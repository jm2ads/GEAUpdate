using Business.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IBanderaService
    {
        Task<List<Bandera>> GetBanderas();

        Task Save(List<Bandera> bandera);
        Task Save(Bandera bandera);
        Task Update(Bandera bandera);
        Task UpdateAll(List<Bandera> banderas);
        Task<Bandera> GetWithChildrenDBAsync();

        Task<Bandera> GetByIdDB(int idBandera);

        Task<Bandera> GetDB();

        Task<List<Bandera>> GetAllDB();

        Task Delete();
        Task Delete(Bandera bandera);
        Task Format();
    }
}
