using Business.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface INegocioService
    {
        Task<List<Negocio>> GetNegocios();

        Task Save(List<Negocio> negocio);

        Task<Negocio> GetWithChildrenDBAsync();

        Task<Negocio> GetByIdDB(int IdNegocio);

        Task<Negocio> GetDB();

        Task Delete();
        Task Format();
        Task UpdateAll(List<Negocio> negocios);
        Task Update(Negocio negocio);

    }
}
