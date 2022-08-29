using Commons.Bootstrapper;
using Commons.Commons.Constants;
using Frontend.Mobile.Areas.Sync.ViewModels;
using Frontend.Mobile.Areas.Sync.ViewModels.Interfaces;
using Frontend.Mobile.Services;
using Rg.Plugins.Popup.Pages;
using Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Mobile.Areas.Sync.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncView : PopupPage
    {
        #region Services

        private readonly ISyncViewModel syncViewModel;

        #endregion
        public SyncView()
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            BindingContext = this.syncViewModel = ContainerManager.Resolve<ISyncViewModel>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await BarraDeProgreso.ProgressTo(0, 250, Easing.Linear);
            EtapaProceso.Text = ApplicationMessages.GettingRows;
            
            var successData = await syncViewModel.GetData();

            if (successData)
            {
                await BarraDeProgreso.ProgressTo(0.2, 250, Easing.Linear);
                EtapaProceso.Text = ApplicationMessages.SavingDB;
                var successSavingDB = await syncViewModel.GuardarDB();
                if (successSavingDB)
                {
                    await BarraDeProgreso.ProgressTo(1, 250, Easing.Linear);
                }
              
                if (successData && successSavingDB)
                {
                    #region ASOSA CerrarPantallasinPopUp 
                    await syncViewModel.CerrarPantallasinPopUp();
                   #endregion
                  //  await syncViewModel.CerrarPantalla();
                }
            }
          
        }
    }
}