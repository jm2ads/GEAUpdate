using Business.Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IAvanceLocalService
    {

        Task Save(AvanceLocal avance);

        Task Delete();

        Task<List<AvanceLocal>> GetAllDB();

        Task<AvanceLocal> GetByTareaPartidaPlanoDB(int idTarea, int idPartida, int idPlano);

        void Send(List<AvanceLocal> avances);

        Task Format();

    }
}
