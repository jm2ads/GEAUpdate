using Frontend.Mobile.Areas.Notificaciones.ViewModels.Interfaces;
using Frontend.Mobile.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.Notificaciones.ViewModels
{
    public class NotificacionViewModel : BaseViewModel, INotificacionViewModel
    {
        #region Commands
        public ICommand CommandNotificacionCancel { get; set; }
        public ICommand CommandNotificacionOK { get; set; }
        #endregion

        #region Actions
        public Action CallbackOKButton;
        public Action CallbackCancelButton;
        #endregion


        #region Properties
        public string ImageNotification { get; set; }
        public string TitleText { get; set; }
        public string NotificationText { get; set; }
        public string BtnTextNotificationCancel { get; set; }
        public string BtnTextNotificationOK { get; set; }
        public bool HasTextCancel { get; set; }
        #endregion

        public NotificacionViewModel()
        {
            CommandNotificacionCancel = new Command(async () => await CancelModal());
            CommandNotificacionOK = new Command(async () => await OKModal());
        }

        public override async Task InitializeAsync(object data)
        {
            Dictionary<string, object> keyValuePairs = (Dictionary<string, object>)data;
            ImageNotification = (string)keyValuePairs["ImageNotificacion"];
            TitleText = (string)keyValuePairs["TitleText"];
            NotificationText = (string)keyValuePairs["NotificationText"];
            BtnTextNotificationOK = (string)keyValuePairs["BtnTextNotificationOK"];

            if (keyValuePairs.ContainsKey("BtnTextNotificationCancel"))
            {
                BtnTextNotificationCancel = (string)keyValuePairs["BtnTextNotificationCancel"];
                HasTextCancel = keyValuePairs.ContainsKey("BtnTextNotificationCancel");
            }

            if (keyValuePairs.ContainsKey("CallbackOKButton"))
                CallbackOKButton += (Action)keyValuePairs["CallbackOKButton"];

            if (keyValuePairs.ContainsKey("CallbackCancelButton"))
                CallbackCancelButton += (Action)keyValuePairs["CallbackCancelButton"];
        }

        private async Task CancelModal()
        {
            CallbackCancelButton?.Invoke();
            if (Navigation.HasPagesInPopupStack())
            {
                await Navigation.PopPopUpAsync();
            }
        }

        private async Task OKModal()
        {
            CallbackOKButton?.Invoke();
            if (Navigation.HasPagesInPopupStack())
            {
                await Navigation.PopPopUpAsync();
            }
        }

    }
}
