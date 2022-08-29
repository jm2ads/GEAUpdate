using Commons.Bootstrapper;
using Frontend.Mobile.Areas.Componentes.ViewModels;
using Frontend.Mobile.Areas.Notificaciones.ViewModels;
using Frontend.Mobile.Areas.SyncDialog.ViewModels;
using Frontend.Mobile.Commons.Models;
using Frontend.Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Frontend.Mobile.Services
{
    public class AlertService
    {
        public async static Task DisplayError(string message = "Ups... hubo un error")
        {
            await Application.Current.MainPage.DisplayAlert("", message, "ACEPTAR");
        }

        public static void DisplayAlert(string title, string message, string cancel = "ACEPTAR")
        {
            Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async static Task DisplayAlertAsync(string title, string message, string cancel = "ACEPTAR")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public static void DisplayLoginError(string errorDescription)
        {
            Application.Current.MainPage.DisplayAlert("", errorDescription, "ACEPTAR");
        }

        public static void DisplayValidationError(string validationMessage = "Los datos cargados son incorrectos, por favor verifique los valores ingresados")
        {
            Application.Current.MainPage.DisplayAlert("", validationMessage, "ACEPTAR");
        }

        public async static Task<bool> DisplayConfirmation(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "ACEPTAR", "CANCELAR");
        }

        public async static Task<bool> DisplayConfirmation(string title, string message, string affirmative, string negative)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, affirmative, negative);
        }

        public async static Task<string> DisplayActionSheet(string title, params string[] options)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, "CANCELAR", null, options);
        }

        public async static Task DisplayCustomAlertConfirmation(string image, string title, string message, string affirmative, string negative = null, Action actionOKButton = null, Action actionCancelButton = null)
        {
            INavigationService Navigation = ContainerManager.Resolve<INavigationService>();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("ImageNotificacion", image);
            keyValuePairs.Add("TitleText", title);
            keyValuePairs.Add("NotificationText", message);

            if (!string.IsNullOrEmpty(affirmative))
                keyValuePairs.Add("BtnTextNotificationOK", affirmative);

            if (!string.IsNullOrEmpty(negative))
                keyValuePairs.Add("BtnTextNotificationCancel", negative);

            if (actionOKButton != null)
                keyValuePairs.Add("CallbackOKButton", actionOKButton);

            if (actionCancelButton != null)
                keyValuePairs.Add("CallbackCancelButton", actionCancelButton);

            await Navigation.PushPopUpAsync<NotificacionViewModel>(keyValuePairs);
        }

        public async static Task DisplayCustomPicker(string[] images, string[] options, string affirmative, Action<string> actionOKButton)
        {
            INavigationService Navigation = ContainerManager.Resolve<INavigationService>();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].Contains("todos"))
                    keyValuePairs.Add("OpcionTodosImage", images[i]);

                if (images[i].Contains("sincronizados"))
                    keyValuePairs.Add("OpcionSincronizadosImage", images[i]);

                if (images[i].Contains("pendienteSincronizar"))
                    keyValuePairs.Add("OpcionPendienteSincronizarImage", images[i]);

                if (images[i].Contains("errorSincronizar"))
                    keyValuePairs.Add("OpcionErrorSincronizarImage", images[i]);

                if (images[i].Contains("selectionState"))
                    keyValuePairs.Add("SelectedOptionIcon", images[i]);

                if (images[i].Contains("manualSync"))
                    keyValuePairs.Add("OpcionManualImage", images[i]);
            }

            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].Equals("Todos"))
                    keyValuePairs.Add("OpcionTodos", options[i]);

                if (options[i].Equals("Sincronizados"))
                    keyValuePairs.Add("OpcionSincronizados", options[i]);

                if (options[i].Equals("Pendiente de Sincronizar"))
                    keyValuePairs.Add("OpcionPendienteSincronizar", options[i]);

                if (options[i].Equals("Error al Sincronizar"))
                    keyValuePairs.Add("OpcionErrorSincronizar", options[i]);

                if (options[i].Equals("Sincronización manual"))
                    keyValuePairs.Add("OpcionManual", options[i]);

            }

            if (!string.IsNullOrEmpty(affirmative))
                keyValuePairs.Add("BtnTextPickerOK", affirmative);

            if (actionOKButton != null)
                keyValuePairs.Add("CallbackOKButton", actionOKButton);

            await Navigation.PushPopUpAsync<PickerViewModel>(keyValuePairs);
        }
        public static Task DisplayCustomAlertSyncronizationConfirmation(string image,string title, string syncronizedInteractionsText, string defaultSyncronizedInteractionsText, string deletedCompetitorsText, string errorInteractionsText, string errorInteractions, string deletedCompetitors, string btnOKText)
        {
            INavigationService Navigation = ContainerManager.Resolve<INavigationService>();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("ImageNotification", image);
            keyValuePairs.Add("TitleText", title);
            keyValuePairs.Add("SyncronizedInteractionsText", syncronizedInteractionsText);
            keyValuePairs.Add("DefaultSyncronizedInteractionsText", defaultSyncronizedInteractionsText);
            keyValuePairs.Add("ErrorInteractionsText", errorInteractionsText);
            keyValuePairs.Add("ErrorInteractions", errorInteractions);
            keyValuePairs.Add("DeletedCompetitorsText", deletedCompetitorsText);
            keyValuePairs.Add("DeletedInteractions", deletedCompetitors);
            keyValuePairs.Add("BtnOKText", btnOKText);

            return Navigation.PushPopUpAsync<SyncDialogViewModel>(keyValuePairs);
        }

    }
}
