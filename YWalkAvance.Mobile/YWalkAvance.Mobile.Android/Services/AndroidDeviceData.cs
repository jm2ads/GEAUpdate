using Android.OS;
using Commons.Commons.Interfaces;
using Frontend.Mobile.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDeviceData))]
namespace Frontend.Mobile.Droid.Services
{
    public class AndroidDeviceData : IDeviceInformation
    {
        public string GetSerial()
        {
            return Build.Serial;
        }

        public string GetManufacturer()
        {
            return Build.Manufacturer;
        }

        public string GetModel()
        {
            return Build.Model;
        }

        public string GetUuid()
        {
            return Build.Serial + Build.Id;
        }

        public string Manufacturer()
        {
            return Build.Manufacturer;
        }

        public string GetVersion()
        {
            return (Build.VERSION.Release).ToString();

        }
    }
}