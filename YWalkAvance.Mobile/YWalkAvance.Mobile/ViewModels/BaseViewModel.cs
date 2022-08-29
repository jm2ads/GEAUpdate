using Commons.Bootstrapper;
using Frontend.Mobile.Services.Interfaces;
using Frontend.Mobile.Views;
using Rg.Plugins.Popup.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Frontend.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public BaseViewModel()
        {
            Navigation = ContainerManager.Resolve<INavigationService>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public INavigationService Navigation;

        bool isBusy = false;
        public bool IsBusy { get; set; }

        string title = string.Empty;
        public string Title { get; set; }

        public async Task StartSpinner(string message = null)
        {
            await PopupNavigation.Instance.PushAsync(new SpinnerView(message), true);
        }

        public async Task StopSpinner()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual async Task InitializeAsync(object data)
        {

        }
    }
}
