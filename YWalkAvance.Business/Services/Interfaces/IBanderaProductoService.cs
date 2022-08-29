using Business.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IBanderaProductoService
    {
        Task<List<BanderaProducto>> GetBanderasProducto();
        Task Save(List<BanderaProducto> bandera);
        Task Save(BanderaProducto banderaProducto);
        Task Update(BanderaProducto bandera);
        Task UpdateAll(List<BanderaProducto> banderas);

        Task<BanderaProducto> GetWithChildrenDBAsync();

        Task<BanderaProducto> GetDB();
        Task<List<BanderaProducto>> GetAllDB();
        Task Delete();
        Task Delete(BanderaProducto banderaProducto);
        Task Format();
    }
}
