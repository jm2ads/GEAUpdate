using Business.Dominio;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Services.Commons;
using Storage.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AvanceLocalService : IAvanceLocalService
    {

        #region Services

        private readonly IRepository<AvanceLocal> repository;

        #endregion

        #region Constructors

        public AvanceLocalService(IRepository<AvanceLocal> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Methods

        public async Task Format()
        {
            await repository.Format();
        }

        public async Task Delete()
        {
            await repository.Delete();
        }

        public async Task Save(AvanceLocal avance)
        {
            await repository.Save(avance);
        }

        public async Task<List<AvanceLocal>> GetAllDB()
        {
            return await repository.GetAllWithChildren();
        }

        public async Task<AvanceLocal> GetByTareaPartidaPlanoDB(int idTarea, int idPartida, int idPlano)
        {
            var avances = await repository.Where(x => x.TareaID == idTarea && x.PartidaID == idPartida && x.PlanoID == idPlano);
            return avances.FirstOrDefault();
        }

        public void Send(List<AvanceLocal> avances)
        {
            HttpClientService.Post(ApiConstants.SetTareasPartidasPlanos, avances);
        }

        #endregion

    }
}
