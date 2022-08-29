using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Commons.Interfaces;
using Foundation;
using Frontend.Mobile.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IOSApplication))]
namespace Frontend.Mobile.iOS.Services
{
    public class IOSApplication : ICloseApplicationIOS
    {
        public void CloseApplication()
        {
            NSThread.Exit();
        }
    }
}