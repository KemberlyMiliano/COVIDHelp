using COVIDHelp.Models;
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
        public static bool IsNotConnected { get; set; }
        static Setting()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyCacheKey;
        }
        public static int SaveInt(this int value, string key)
        {
            Preferences.Set(key, value);
            return 1;
        }
        public static int GetPreferencesInt(this int value, string key )
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
        public static bool MapsSetting
        {
            get
            {
                Barrel.ApplicationId = ConfigApi.MonkeyCacheKey;
                return Barrel.Current.Exists(nameof(MapsSetting));

            }
        }

        public static long PhoneSetting {
            get {
                try
                {
                    return Convert.ToInt64(SecureStorage.GetAsync(Constants.PhoneKey).Result);
                }
                catch (Exception)
                {

                 return Barrel.Current.Get<long>(Constants.PhoneKey);
                }

            }
            set
            {
                try
                {
                    SecureStorage.SetAsync(Constants.PhoneKey,$"{value}");
                }
                catch (Exception)
                {

                    Barrel.Current.Add(key: Constants.PhoneKey, data: value, expireIn: TimeSpan.FromDays(20));
                }

            }   
        }

        public static int Id
        {
            get
            {
                try
                {
                     return Convert.ToInt32(SecureStorage.GetAsync(Constants.IdKey).Result);
                }
                catch (Exception)
                {
                    return Barrel.Current.Get<int>(Constants.IdKey);
                }
             
            }
            set
            {
                try
                {
                    SecureStorage.SetAsync(Constants.IdKey, $"{value}");
                }
                catch (Exception)
                {

                    Barrel.Current.Add(key: Constants.IdKey, data: value, expireIn: TimeSpan.FromDays(20));
                }

            }
        }
        public static string Token
        {
            get
            {
                try
                {
                    return SecureStorage.GetAsync(Constants.Token).Result;
                }
                catch (Exception)
                {
                    return Barrel.Current.Get<string>(Constants.Token);
                }
               
            }
            set
            {
                try
                {
                    SecureStorage.SetAsync(Constants.Token, $"Bearer {value}");
                }
                catch (Exception)
                {

                    Barrel.Current.Add(key: Constants.Token, data: $"Bearer {value}", expireIn: TimeSpan.FromDays(20));
                }
                

            }
        }
        public async static Task<string> Latitude() {

            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location!=null)
            {
                return $"{location.Latitude}";
            }
            return "0,0";

        }
        public async static Task<string> Longitude()
        {

            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                return $"{location.Longitude}";
            }
            return "0,0";
        }
        public static void CloseLogged()
        {
            try
            {
                SecureStorage.RemoveAll();
                Barrel.Current.EmptyAll();
            }
            catch (Exception)
            {
                Barrel.Current.EmptyAll();
            }

        }
    }
}
