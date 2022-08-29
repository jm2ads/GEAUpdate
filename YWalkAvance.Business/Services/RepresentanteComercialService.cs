using Business.Dominio;
using Business.Services.Interfaces;
using Commons.Commons.Constants;
using Services.Commons;
using Storage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RepresentanteComercialService : IRepresentanteComercialService
    {
        private readonly IRepository<RepresentanteComercial> repository;

        public RepresentanteComercialService(IRepository<RepresentanteComercial> repository)
        {
            this.repository = repository;
        }


        public Task Delete()
        {
            return repository.Delete();
        }

        public Task Format()
        {
            return repository.Format();
        }

        public Task<RepresentanteComercial> GetDB()
        {
            return repository.First();
        }

        public async Task<IList<RRCC>> GetRepresentanteComercial(string idRed)
        {
            #region ASOSA ELIMINO VERIFICACION DE  RRCC
           //List<RRCC> RRCCList = new List<RRCC>();
           // RRCC rRCC = new RRCC();
           // rRCC.Usuario = idRed;
           
           // try
           // {
           //     RRCCList.Add(rRCC);
           //     return RRCCList;
           // }
           // catch (Exception e)
           // {
           //     throw e;
           // }
          
            #endregion
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>(){
                       new KeyValuePair<string, string>("idRed", idRed.ToString())
            };

            LlamadaRFC_RRCC llamadaRfc = await HttpClientService.GetRRCC<LlamadaRFC_RRCC>(ApiConstants.GetRepresentanteComercial, values);

            return llamadaRfc.RRCC;
        }

        public async Task<RepresentanteComercial> GetOneFromDB(string codigoInterlocutor)
        {
            var rrccList = await repository.Query("SELECT * FROM RepresentanteComercial WHERE CodigoInterlocutor = ?", codigoInterlocutor);
            return rrccList.FirstOrDefault();
        }

        public async Task<RepresentanteComercial> GetWithChildrenDBAsync()
        {
            var RRCC = await repository.GetAllWithChildren();
            return RRCC.First();
        }

        public Task Save(RepresentanteComercial RRCC)
        {
            return repository.Save(RRCC);
        }

        public Task Update(RepresentanteComercial RRCC)
        {
            return repository.Update(RRCC);
        }

    }
}
