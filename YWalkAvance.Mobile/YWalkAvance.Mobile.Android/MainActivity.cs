using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Commons.Bootstrapper;
using FFImageLoading.Svg.Forms;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Xam.Plugin.Droid;

namespace Frontend.Mobile.Droid
{
    [Activity(Label = "Relevamiento de Precios", Icon = "@drawable/mainIcon", Theme = "@style/splashscreen", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //SyncfusionLicenseProvider.RegisterLicense("MTgxMjgyQDMxMzcyZTMzMmUzMGRXemlqTlZIcHM5QWpJZUlIZVN6OElRWTZxT0EzSjJydHZMR1pZVFRIVkU9");

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Instance = this;
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            base.SetTheme(Resource.Style.MainTheme);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Plugin.Iconize.Iconize.Init(ToolbarResource, TabLayoutResource);
            var ignore = typeof(SvgCachedImage);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            PopupEffect.Init();
            var bootstraperStartup = new Startup();
            LoadApplication(new App(bootstraperStartup));

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}