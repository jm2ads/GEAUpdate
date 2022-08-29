using Commons.Commons.Interfaces;
using Frontend.Mobile.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IosDeviceData))]
namespace Frontend.Mobile.iOS.Services
{
    public class IosDeviceData : IDeviceInformation
    {
        public string GetSerial()
        {
            return GetUuid();
        }

        public string GetManufacturer()
        {
            return "apple";
        }

        public string GetModel()
        {
            return UIDevice.CurrentDevice.Model;
        }

        public string GetUuid()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }
        public string GetVersion()
        {
            return UIDevice.CurrentDevice.SystemVersion;
        }
    }
}