using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Business.Dominio;
using Business.Services.Interfaces;
using Commons.Commons.Interfaces;
using Frontend.Mobile.Areas.RepresentantesComerciales.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Helpers;
using Frontend.Mobile.Commons.Models;
using Frontend.Mobile.ViewModels;
using Microsoft.AppCenter.Crashes;
using Services.Commons;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.RepresentantesComerciales.ViewModels
{
    public class RepresentanteComercialViewModel : BaseViewModel, IRepresentanteComercialViewModel
    {
        #region Services
        public IRepresentanteComercialService RepresentanteComercialService;
        public INegocioService NegocioService;
        public ISegmentoService SegmentoService;
        private IAppVersion Version { get; set; }
        #endregion
        #region Commands

        public ICommand ExitPopupCommand { get; set; }

        #endregion

        #region Properties
        private string _buildVersion;
        public string BuildVersion
        {
            get
            {
                return _buildVersion;
            }
            set
            {
                _buildVersion = value;
            }

        }
        #endregion

        #region Constructors
        public RepresentanteComercialViewModel(IRepresentanteComercialService representanteComercialService, INegocioService negocioService, ISegmentoService segmentoService)
        {
            RepresentanteComercialService = representanteComercialService;
            NegocioService = negocioService;
            SegmentoService = segmentoService;
            ExitPopupCommand = new Command(async () => await CloseModal());
            Version = DependencyService.Get<IAppVersion>();
            BuildVersion = Version.GetBuildVersion();
        }
        #endregion

        #region Methods
        private async Task CloseModal()
        {
            if (Navigation.HasPagesInPopupStack())
                await Navigation.PopPopUpAsync();
        }
        //ASOSA VER RRCC EN ABOUT
        //public async Task<RepresentanteComercialGroupModel> GetRepresentanteComercial(string idRed)
        //{
        //    RepresentanteComercialGroupModel representanteComercial = null;
        //    Negocio negocio = null;
        //    Segmento segmento = null;
        //    try
        //    {
               
        //        var rrccDB = await RepresentanteComercialService.GetDB();

        //        negocio = await NegocioService.GetByIdDB(int.Parse(rrccDB.IdNegocio));
        //        segmento = await SegmentoService.GetByIdDB(negocio.IdSegmento);

        //        representanteComercial = new RepresentanteComercialGroupModel()
        //        {
        //            Nombre = rrccDB.Nombre,
        //            Apellido = rrccDB.Apellido,
        //            Usuario = rrccDB.Usuario,
        //            CodigoInterlocutor = rrccDB.CodigoInterlocutor,
        //            IdNegocio = segmento.Descripcion,
        //        };
        //    }
        //    catch (Exception e)
        //    {
        //        Crashes.TrackError(e);
        //        throw e;
        //    }

        //    return representanteComercial;
        //}

        public string GetBuildVersion()
        {
            return BuildVersion;
        }
        #endregion

    }
}
