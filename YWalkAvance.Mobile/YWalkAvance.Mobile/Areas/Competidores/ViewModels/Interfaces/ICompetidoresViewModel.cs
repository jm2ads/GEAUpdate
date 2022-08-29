using Business.Dominio;
using Frontend.Mobile.Commons.Components;
using Frontend.Mobile.Commons.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.Competidores.ViewModels.Interfaces
{
    public interface ICompetidoresViewModel
    {
        void CompetidoresListView_ItemTapped(object sender, ItemTappedEventArgs e);
        void InputSearch_TextChanged(object sender, TextChangedEventArgs e);
        void TapGestureRecognizer_Tapped(object sender, EventArgs e);
        //void PickerState_SelectedIndexChanged(object sender, EventArgs e);
        void SetListViewCompetidores(ListView listViewCompetidores);
        Task<ObservableCollection<CompetidoresGroupModel>> GetCompetidores(List<Competidor> competidoresSAP = null);
        void SetInputSearch(SearchBar input);
        void SetBtnSincronizarPrecios(Button syncPrices);
        void SetBtnNuevoRelevamiento(ImageButton btnNuevoRelevamiento);
        void SetPickerState(ImageButton picker);
        Task InitializeCompetidores();

        void SyncSingleCompetitorCommand(object sender, EventArgs e);

        //bool HasPrizesToSync();
    }
}
