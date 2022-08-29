using Commons.Commons.Interfaces;
using Foundation;
using Frontend.Mobile.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSAppVersion))]
namespace Frontend.Mobile.iOS.Services
{
    public class IOSAppVersion : IAppVersion
    {
        public IOSAppVersion()
        {
        }

        public string GetBuildVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }

        public int GetNumberVersion()
        {
            return int.Parse(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString());
        }
    }
}
