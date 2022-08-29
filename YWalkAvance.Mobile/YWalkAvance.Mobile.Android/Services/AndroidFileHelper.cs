using Frontend.Mobile.Droid.Services;
using Storage.Commons.Interfaces;
using System.IO;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(AndroidFileHelper))]
namespace Frontend.Mobile.Droid.Services
{
    public class AndroidFileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}