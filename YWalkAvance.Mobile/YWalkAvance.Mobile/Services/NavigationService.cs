using Frontend.Mobile.Services.Interfaces;
using Frontend.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Frontend.Mobile.Services
{
    public class NavigationService : INavigationService
    {

        #region Metodos Publicos

        public Task PushPopUpAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigatePopUpToAsync(typeof(TViewModel), null);
        }

        public Task PushPopUpAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigatePopUpToAsync(typeof(TViewModel), parameter);
        }

        public async Task PopPopUpAsync()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public bool HasPagesInPopupStack()
        {
            return PopupNavigation.Instance.PopupStack.Any();
        }

        public Task PushAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task PushAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public async Task PopAsync()
        {
            var CurrentPage = Application.Current.MainPage as NavigationPage;
            await CurrentPage.PopAsync();
        }

        #endregion

        #region Metodos Privados

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);
            var CurrentPage = Application.Current.MainPage as NavigationPage;
            await CurrentPage.PushAsync(page);
            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private async Task InternalNavigatePopUpToAsync(Type viewModelType, object parameter)
        {
            PopupPage popup = CreatePopUp(viewModelType, parameter);
            await PopupNavigation.Instance.PushAsync(popup);
            await (popup.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
                throw new System.Exception("Error");

            Page page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

        private PopupPage CreatePopUp(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
                throw new System.Exception("Error");

            PopupPage popup = Activator.CreateInstance(pageType) as PopupPage;

            return popup;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private void InternalInitialPush(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, null);
            (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
            Application.Current.MainPage = new NavigationPage(page);
        }
        public void InitialPush<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            InternalInitialPush(typeof(TViewModel), parameter);
        }

        public void InitialPush<TViewModel>() where TViewModel : BaseViewModel
        {
            InternalInitialPush(typeof(TViewModel), null);
        }

        #endregion

    }
}
