using System;
using System.IO;
using Frontend.Mobile.iOS.Services;
using Storage.Commons.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSFileHelper))]
namespace Frontend.Mobile.iOS.Services
{
    public class IOSFileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, filename);
        }
    }
}
