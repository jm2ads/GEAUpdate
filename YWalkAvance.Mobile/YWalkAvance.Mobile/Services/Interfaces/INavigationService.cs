using Frontend.Mobile.ViewModels;
using System.Threading.Tasks;

namespace Frontend.Mobile.Services.Interfaces
{
    public interface INavigationService
    {

        Task PushAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task PushPopUpAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task PushPopUpAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task PushAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task PopAsync();
        Task PopPopUpAsync();
        bool HasPagesInPopupStack();
        void InitialPush<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        void InitialPush<TViewModel>() where TViewModel : BaseViewModel;

    }
}
