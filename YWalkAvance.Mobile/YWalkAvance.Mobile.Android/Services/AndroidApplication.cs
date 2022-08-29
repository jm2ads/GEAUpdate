using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Commons.Commons.Interfaces;
using Frontend.Mobile.Droid.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidApplication))]
namespace Frontend.Mobile.Droid.Services
{
    public class AndroidApplication : ICloseApplication
    {
        public void CloseApplication()
        {
            var activity = MainActivity.Instance;
            activity.FinishAffinity();
        }

        public void OnBackPressed()
        {
            var activity = MainActivity.Instance;
            activity.OnBackPressed();
        }
    }
}