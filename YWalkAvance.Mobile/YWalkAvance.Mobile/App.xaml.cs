using System;
using System.Globalization;
using Commons.Bootstrapper;
using Frontend.Mobile.ViewModels.Interfaces;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Plugin.Iconize;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("din-bold.ttf", Alias = "din-bold")]
[assembly: ExportFont("din-light.ttf", Alias = "din-light")]
[assembly: ExportFont("din-medium.ttf", Alias = "din-medium")]
[assembly: ExportFont("din-regular.ttf", Alias = "din-regular")]
[assembly: ExportFont("OpenSans-Bold.ttf", Alias = "OpenSans-Bold")]
[assembly: ExportFont("OpenSans-Light.ttf", Alias = "OpenSans-Light")]
[assembly: ExportFont("OpenSans-Regular.ttf", Alias = "OpenSans-Regular")]
[assembly: ExportFont("OpenSans-Semibold.ttf", Alias = "OpenSans-Semibold")]
namespace Frontend.Mobile
{
    public partial class App : Application
    {
        private readonly IAppViewModel vm;

        public App(IBootstraperStartup bootstraperStartup)
        {
            try
            {
                CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

                InitializeComponent();
               // SetCultureToUSEnglish();
                Akavache.Registrations.Start("GEARRPP");

                bootstraperStartup.ConfigureContainer();
                Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule())
                       .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule());
                BindingContext = vm = ContainerManager.Resolve<IAppViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void OnStart()
        {
            SetCultureToUSEnglish();
            base.OnStart();
            AppCenter.Start("android=03046af8-7938-46fd-8b51-4109f9fc7e4f;" +
            "ios=0e12d7b7-64b6-4870-a208-9366e7383e7d", typeof(Crashes));
        }
        protected override void OnResume()
        {
            SetCultureToUSEnglish();
            base.OnResume();
        }
        private void SetCultureToUSEnglish()
        {
             CultureInfo englishUSCulture = new CultureInfo("en-US",false);
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
            CultureInfo.DefaultThreadCurrentUICulture = englishUSCulture;


           

        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
            base.OnSleep();
        }
    }
}
