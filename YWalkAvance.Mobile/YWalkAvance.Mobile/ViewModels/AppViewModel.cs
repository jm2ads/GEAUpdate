using Commons.Bootstrapper;
using Frontend.Mobile.Areas.Competidores.Views;
using Frontend.Mobile.Areas.Login.Views;
using Frontend.Mobile.ViewModels.Interfaces;
using Xamarin.Forms;

namespace Frontend.Mobile.ViewModels
{
    public class AppViewModel : IAppViewModel
    {
        public AppViewModel()
        {
            //Application.Current.MainPage = new NavigationPage(ContainerManager.Resolve<CompetidoresView>());
            Application.Current.MainPage = new NavigationPage(ContainerManager.Resolve<LoginView>());
        }
    }
}
