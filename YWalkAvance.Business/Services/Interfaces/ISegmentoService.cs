using Business.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface ISegmentoService
    {
        Task<List<Segmento>> GetSegmentos();

        Task Save(List<Segmento> segmento);
        Task Update(Segmento segmento);
        Task UpdateAll(List<Segmento> segmentos);

        Task<Segmento> GetWithChildrenDBAsync();

        Task<Segmento> GetByIdDB(int IdSegmento);

        Task<Segmento> GetDB();

        Task Delete();
        Task Format();
    }
}
