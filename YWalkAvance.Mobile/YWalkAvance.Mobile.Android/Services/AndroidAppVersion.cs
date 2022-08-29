using Android.Content.PM;
using Commons.Commons.Interfaces;
using Frontend.Mobile.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidAppVersion))]
namespace Frontend.Mobile.Droid.Services
{
    public class AndroidAppVersion : IAppVersion
    {
        public string GetBuildVersion()
        {
            var context = global::Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public int GetNumberVersion()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode;
        }
    }
}