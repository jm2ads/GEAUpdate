using Frontend.Mobile.Areas.SyncDialog.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Models;
using Frontend.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.SyncDialog.ViewModels
{
    public class SyncDialogViewModel : BaseViewModel, ISyncDialogViewModel
    {
        
        public ICommand OKButtonCommand { get; set; }
        public string ImageNotification { get; set; }
        public string TitleText { get; set; }
        public string BtnOKText { get; set; }
        public string SyncronizedInteractionsText { get; set; }
        public string DefaultSyncronizedInteractionsText { get; set; }
        public string DeletedCompetitorsText { get; set; }
        public string ErrorInteractionsText { get; set; }
        public string DeletedInteractions { get; set; }
        public string ErrorInteractions { get; set; }
        public bool IsVisibleDeletedCompetitors { get; set; }
        public bool IsVisibleErrorInteractions { get; set; }
        public bool IsVisibleSyncronizedInteractions { get; set; }
        public bool IsVisibleDefaultSyncronizedInteractions { get; set; }

        private ObservableCollection<string> _deletedCompetitorsList;
        public ObservableCollection<string> DeletedCompetitorsList
        {
            get { return _deletedCompetitorsList; }
            set
            {
                _deletedCompetitorsList = value;
                OnPropertyChanged("DeletedCompetitorsList");
            }
        }
        private ObservableCollection<string> _errorInteractionsList;
        public ObservableCollection<string> ErrorInteractionsList
        {
            get { return _errorInteractionsList; }
            set
            {
                _errorInteractionsList = value;
                OnPropertyChanged("ErrorInteractionsList");
            }
        }
        public SyncDialogViewModel() {
            OKButtonCommand = new Command(async () => await OKButton());
        }

        private async Task OKButton()
        {
            if (Navigation.HasPagesInPopupStack())
                await Navigation.PopPopUpAsync();
        }

        public override async Task InitializeAsync(object data)
        {
            Dictionary<string, object> keyValuePairs = (Dictionary<string, object>)data;
            ImageNotification = (string)keyValuePairs["ImageNotification"];
            TitleText = (string)keyValuePairs["TitleText"];
            BtnOKText = (string)keyValuePairs["BtnOKText"];
            SyncronizedInteractionsText = (string)keyValuePairs["SyncronizedInteractionsText"];
            DeletedCompetitorsText = (string)keyValuePairs["DeletedCompetitorsText"];
            ErrorInteractionsText = (string)keyValuePairs["ErrorInteractionsText"];
            DeletedInteractions = (string)keyValuePairs["DeletedInteractions"];
            ErrorInteractions = (string)keyValuePairs["ErrorInteractions"];
            DefaultSyncronizedInteractionsText = (string)keyValuePairs["DefaultSyncronizedInteractionsText"];
            IsVisibleDeletedCompetitors = !String.IsNullOrEmpty(DeletedInteractions);
            IsVisibleErrorInteractions = !String.IsNullOrEmpty(ErrorInteractions);
            IsVisibleSyncronizedInteractions = !String.IsNullOrEmpty(SyncronizedInteractionsText);
            IsVisibleDefaultSyncronizedInteractions = !IsVisibleDeletedCompetitors && !IsVisibleErrorInteractions && !IsVisibleSyncronizedInteractions;
        }

    }
}
