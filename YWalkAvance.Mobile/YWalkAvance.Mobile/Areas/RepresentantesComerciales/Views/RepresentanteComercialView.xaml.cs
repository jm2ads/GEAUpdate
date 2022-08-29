using Commons.Bootstrapper;
using Frontend.Mobile.Areas.RepresentantesComerciales.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Models;
using Rg.Plugins.Popup.Pages;
using Services.Commons;
using Xamarin.Forms.Xaml;

namespace Frontend.Mobile.Areas.RepresentantesComerciales.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RepresentanteComercialView : PopupPage
	{
        #region Services

        private readonly IRepresentanteComercialViewModel RRCCViewModel;

        #endregion

        public RepresentanteComercialView ()
		{
            CloseWhenBackgroundIsClicked = true;
            InitializeComponent ();
            BindingContext = this.RRCCViewModel = ContainerManager.Resolve<IRepresentanteComercialViewModel>();
        }

        protected async override void OnAppearing() {

            #region ASOSA CAMBIO RRCC X UserInfo

            //string idRed = await LocalStorageService.GetUserLogin();
            // RepresentanteComercialGroupModel repComercial = await RRCCViewModel.GetRepresentanteComercial(idRed);
            //NombreApellido.Text = repComercial.NombreApellido;
            //Usuario.Text = repComercial.Usuario;
            //ID.Text = repComercial.CodigoInterlocutor;
            //UnidadNegocio.Text = repComercial.IdNegocio;


            UserInfo userInfo = await LocalStorageService.GetAccountData();
            NombreApellido.Text = userInfo.UserName;
            Usuario.Text = userInfo.UserLogin;
            //ID.Text = userInfo..CodigoInterlocutor;
            //UnidadNegocio.Text = repComercial.IdNegocio;
            #endregion
            VersionApp.Text = RRCCViewModel.GetBuildVersion();

        }
    }
}