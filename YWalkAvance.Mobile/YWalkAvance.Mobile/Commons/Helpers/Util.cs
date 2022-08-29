using Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Frontend.Mobile.Commons.Helpers
{
    public static class Util
    {
        public static bool HasInternetConnection() {
            var profiles = Connectivity.ConnectionProfiles;
            return profiles.Contains(ConnectionProfile.WiFi) || profiles.Contains(ConnectionProfile.Cellular);
        }

        public static bool HasServiceConnectivity() {
            return HttpClientService.IsAlive();
        }

        public static Task<bool> HasInternetConnectionAsync()
        {
            var profiles = Connectivity.ConnectionProfiles;
            var value = profiles.Contains(ConnectionProfile.WiFi) || profiles.Contains(ConnectionProfile.Cellular);
            return Task.FromResult(value);
        }

        public static async Task<bool> HasServiceConnectivityAsync()
        {
            return await HttpClientService.IsAliveAsync();
        }
    }
}
