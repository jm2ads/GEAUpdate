using Frontend.Mobile.Areas.Componentes.ViewModels.Interfaces;
using Frontend.Mobile.Commons.Helpers;
using Frontend.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.Componentes.ViewModels
{
    public class PickerViewModel : BaseViewModel, IPickerViewModel
    {
        #region Actions
        public Action<string> CallbackOKButton;
        #endregion

        public ICommand CommandPickerOK { get; set; }
        public bool IsOptionTodosSelected { get; set; }
        public bool IsOptionSincronizadosSelected { get; set; }
        public bool IsOptionPendienteSincronizarSelected { get; set; }
        public bool IsOptionErrorSincronizarSelected { get; set; }
        public bool IsOptionManualSelected { get; set; }
        public string SelectedOption { get; set; }
        public string SelectedOptionIcon { get; set; }
        public string OpcionTodosImage { get; set; }
        public string OpcionTodos { get; set; }
        public string OpcionSincronizadosImage { get; set; }
        public string OpcionSincronizados { get; set; }
        public string OpcionPendienteSincronizarImage { get; set; }
        public string OpcionPendienteSincronizar { get; set; }
        public string OpcionErrorSincronizarImage { get; set; }
        public string OpcionErrorSincronizar { get; set; }
        public string OpcionManualImage { get; set; }
        public string OpcionManual { get; set; }


        public string BtnTextPickerOK { get; set; }

        public PickerViewModel() {
            CommandPickerOK = new Command(async () => await OKPicker());
        }

        public override async Task InitializeAsync(object data)
        {
            Dictionary<string, object> keyValuePairs = (Dictionary<string, object>)data;
            SelectedOptionIcon = (string)keyValuePairs["SelectedOptionIcon"];
            OpcionTodosImage = (string)keyValuePairs["OpcionTodosImage"];
            OpcionTodos = (string)keyValuePairs["OpcionTodos"];
            OpcionSincronizadosImage = (string)keyValuePairs["OpcionSincronizadosImage"];
            OpcionSincronizados = (string)keyValuePairs["OpcionSincronizados"];
            OpcionPendienteSincronizarImage = (string)keyValuePairs["OpcionPendienteSincronizarImage"];
            OpcionPendienteSincronizar = (string)keyValuePairs["OpcionPendienteSincronizar"];
            OpcionErrorSincronizarImage = (string)keyValuePairs["OpcionErrorSincronizarImage"];
            OpcionErrorSincronizar = (string)keyValuePairs["OpcionErrorSincronizar"];

            OpcionManualImage = (string)keyValuePairs["OpcionManualImage"];
            OpcionManual = (string)keyValuePairs["OpcionManual"];

            BtnTextPickerOK = (string)keyValuePairs["BtnTextPickerOK"];
            CallbackOKButton += (Action<string>)keyValuePairs["CallbackOKButton"];
        }

        private async Task OKPicker()
        {
            //CallbackOKButton(SelectedOption);
            if (Navigation.HasPagesInPopupStack())
                await Navigation.PopPopUpAsync();
        }

        public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            SelectedOption = "";
            SelectedOption = OpcionTodos;
            IsOptionTodosSelected = true;
            IsOptionSincronizadosSelected = false;
            IsOptionPendienteSincronizarSelected = false;
            IsOptionErrorSincronizarSelected = false;
            IsOptionManualSelected = false;
            CallbackOKButton(SelectedOption);
        }

        public void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            SelectedOption = "";
            SelectedOption = OpcionSincronizados;
            IsOptionTodosSelected = false;
            IsOptionSincronizadosSelected = true;
            IsOptionPendienteSincronizarSelected = false;
            IsOptionErrorSincronizarSelected = false;
            IsOptionManualSelected = false;
            CallbackOKButton(SelectedOption);
        }

        public void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            SelectedOption = "";
            SelectedOption = OpcionPendienteSincronizar;
            IsOptionTodosSelected = false;
            IsOptionSincronizadosSelected = false;
            IsOptionPendienteSincronizarSelected = true;
            IsOptionErrorSincronizarSelected = false;
            IsOptionManualSelected = false;
            CallbackOKButton(SelectedOption);
        }

        public void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            SelectedOption = "";
            SelectedOption = OpcionErrorSincronizar;
            IsOptionTodosSelected = false;
            IsOptionSincronizadosSelected = false;
            IsOptionPendienteSincronizarSelected = false;
            IsOptionErrorSincronizarSelected = true;
            IsOptionManualSelected = false;
            CallbackOKButton(SelectedOption);
        }

        public void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            SelectedOption = "";
            SelectedOption = OpcionManual;
            IsOptionTodosSelected = false;
            IsOptionSincronizadosSelected = false;
            IsOptionPendienteSincronizarSelected = false;
            IsOptionErrorSincronizarSelected = false;
            IsOptionManualSelected = true;
            CallbackOKButton(SelectedOption);
        }

    }
}
