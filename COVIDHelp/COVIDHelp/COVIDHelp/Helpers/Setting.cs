using COVIDHelp.Services;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.Helpers
{
    public static class Setting
    {
        public static int SaveInt(this Int64 value, string key)
        {
            Preferences.Set(key, value);
            return 1;
        }
        public static Int64 GetPreferencesInt(this Int64 value, string key )
        {
            return Preferences.Get(key, value);
        }
        public static string SaveString(this string value, string key)
        {
              Preferences.Set(key, value);
            return "save";
        }
        public static string GetPreferences(this string value,string key)
        {
            return Preferences.Get(key, value);
        }
        public static bool IsRemeberme(this bool remenber,string correo) {

            if (remenber)
            {
                Barrel.ApplicationId = ConfigApi.MonkeyChadeKey;
                Barrel.Current.Add(key:$"Rember/true",data: correo, expireIn:TimeSpan.FromDays(20));
                return true;
            }
            return false;
        }
        public static bool IsCheckRember()
        {

            return Barrel.Current.Exists(key: $"Rember/true");

        }
        public static string Recordar(this string remenber, bool Isremeber)
        {

            if (Isremeber)
            {
                Barrel.ApplicationId = ConfigApi.MonkeyChadeKey;
               return Barrel.Current.Get<string>(key: $"Rember/true");
            }
            return null;
        }
        public async static Task<string> Latitude(this string lat) {

            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location!=null)
            {
                return $"{location.Latitude}";
            }
            return lat;

        }
        public async static Task<string> Longitude(this string lng)
        {

            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                return $"{location.Longitude}";
            }
            return lng;

        }
    }
}
