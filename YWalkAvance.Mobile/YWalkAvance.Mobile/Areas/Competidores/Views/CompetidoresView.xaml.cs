using Commons.Bootstrapper;
using Commons.Commons.Constants;
using Commons.Commons.Entities;
using Commons.Commons.Interfaces;
using Frontend.Mobile.Areas.Competidores.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Models;
using Frontend.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Mobile.Areas.Competidores.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompetidoresView : ContentPage
	{
        private readonly ICompetidoresViewModel competidoresViewModel;
        private ICloseApplication Application { get; set; }

        private ICloseApplicationIOS ApplicationIOS { get; set; }
        public CompetidoresView()
		{
            try
            {
                InitializeComponent();
                Application = DependencyService.Get<ICloseApplication>();
                ApplicationIOS = DependencyService.Get<ICloseApplicationIOS>();
                BindingContext = this.competidoresViewModel = ContainerManager.Resolve<ICompetidoresViewModel>();
                competidoresViewModel.SetInputSearch(InputSearch);
                competidoresViewModel.SetPickerState(PickerState);
                competidoresViewModel.SetBtnSincronizarPrecios(BtnSincronizarPrecios);
                competidoresViewModel.SetBtnNuevoRelevamiento(BtnNuevoRelevamiento);
                competidoresViewModel.SetListViewCompetidores(CompetidoresListView);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            

        }
        protected async override void OnAppearing()
        {
            await this.competidoresViewModel.InitializeCompetidores();
        }

        protected override bool OnBackButtonPressed()
        {
            AlertService.DisplayConfirmation(ApplicationMessages.Exit, ApplicationMessages.ExitApp).ContinueWith(t =>
            {
                if (t.Result)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Application.CloseApplication();
                    }
                    else
                    {
                        ApplicationIOS.CloseApplication();
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
            return true;
        }

        private void CompetidoresListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            competidoresViewModel.CompetidoresListView_ItemTapped(sender, e);
        }

        private void InputSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            competidoresViewModel.InputSearch_TextChanged(sender, e);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            competidoresViewModel.TapGestureRecognizer_Tapped(sender, e);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            competidoresViewModel.SyncSingleCompetitorCommand(sender, e);
        }
    }
}