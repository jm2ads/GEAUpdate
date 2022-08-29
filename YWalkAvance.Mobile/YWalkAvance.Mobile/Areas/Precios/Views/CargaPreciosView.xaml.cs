using Commons.Bootstrapper;
using Commons.Commons.Constants;
using Frontend.Mobile.Areas.Precios.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Helpers;
using Frontend.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Mobile.Areas.Precios.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CargaPreciosView : ContentPage
    {
        private readonly ICargaPreciosViewModel cargaPreciosViewModel;
        public Action ActionPopAsyncOKButton { get; set; }

        public CargaPreciosView()
        {
            try
            {
                InitializeComponent();
                
                
              
                BindingContext = this.cargaPreciosViewModel = ContainerManager.Resolve<ICargaPreciosViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargaPreciosViewModel.SetCollectionViewProductos(ProductosListView);
            cargaPreciosViewModel.SetBtnGuardar(BtnGuardar);
            cargaPreciosViewModel.SetBtnReiniciarPrecios(BtnReiniciarPrecios);
            cargaPreciosViewModel.SetBtnExpandCollapse(BtnExpandCollapse);
            ActionPopAsyncOKButton += CallbackPopAsyncOKButton;
        }

        private void CallbackPopAsyncOKButton()
        {
            EventsManager.TriggerEvent("IsClickedOnceReset");
            Navigation.PopAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private void PrecioEnterosEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            cargaPreciosViewModel.PrecioEnterosEntry_TextChanged(sender, e);


        }

        private void PrecioDecimalesEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            cargaPreciosViewModel.PrecioDecimalesEntry_TextChanged(sender, e);
        }


        protected override bool OnBackButtonPressed()
        {

            cargaPreciosViewModel.HasNotSavedProducts().ContinueWith(t =>
            {
                if (t.Result)
                {
                    AlertService.DisplayCustomAlertConfirmation(ApplicationMessages.WarningDrawable, ApplicationMessages.Atencion, ApplicationMessages.NotSavedProductsNotification, ApplicationMessages.Save, ApplicationMessages.Cancel, ActionPopAsyncOKButton);
                }
                else
                {
                    CallbackPopAsyncOKButton();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return true;
        }

        private void PrecioEnterosEntry_Focused(object sender, FocusEventArgs e)
        {
            cargaPreciosViewModel.PrecioEnterosEntry_Focused(sender, e);
        }

        private void EntryDecimales_Focused(object sender, FocusEventArgs e)
        {
            cargaPreciosViewModel.DecimalesEntry_Focused(sender, e);
        }

        private void EntryPrecio_Unfocused(object sender, FocusEventArgs e)
        {
            cargaPreciosViewModel.EntryPrecio_Unfocused(sender, e);
        }
    }
}