using System;
using Frontend.Mobile.Areas.Login.ViewModels.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Mobile.Areas.Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {

        #region Services

        private readonly ILoginViewModel loginViewModel;

        #endregion

        #region Constructors

        public LoginView(ILoginViewModel loginViewModel)
        {
            try
            {
                InitializeComponent();
                BindingContext = this.loginViewModel = loginViewModel;
                //loginViewModel.SetBtnDeleteUser(BtnDeleteUser);
                loginViewModel.SetInput(InputUser);
                loginViewModel.SetPassInput(InputPass);
                if (Device.RuntimePlatform == Device.iOS)
                {
                    ImageLogin.Margin = new Thickness(0, -10, 0, 0);
                    ImageLogin.Scale = 1.2f;
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    ImageLogin.Margin = new Thickness(8, 0, 0, 0);
                    ImageLogin.Scale = 0.9f;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        protected async override void OnAppearing()
        {
#if DEBUG


            //InputUser.Text = "RY07267";//no anduvo
            //InputPass.Text = "28.10.21Nueva";

            InputUser.Text = "RY08231";//anduvo
            InputPass.Text = "Riquelme2022";


            //InputUser.Text = "se34582 ";
            //InputPass.Text = "Male15122005";



            //InputUser.Text = "RY28171";// anduvo
            //InputPass.Text = "Heladera2021";



















            //    InputUser.Text = "SE30093";
            //InputPass.Text = "PruebasRRPP2056"; 

            //InputUser.Text = "se34582 ";
            //InputPass.Text = "Male15122005";
#endif

            LabelVersion.Text = "Versión: " + loginViewModel.GetVersionCode();
            var syncFinished = await loginViewModel.IsSyncFinished();


            // ASOSA IsSessionClosed ORIGINAL
            //if (syncFinished && !loginViewModel.IsSessionClosed()) {
            var isSessionClosed = await loginViewModel.IsSessionClosed();
            if (syncFinished && !isSessionClosed)
            {
                loginViewModel.CheckLoginCommand.Execute(null);
                //if (!loginViewModel.IsLoggedIn())
                base.OnAppearing();
            }
        }

    }
}