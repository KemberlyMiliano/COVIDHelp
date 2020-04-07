using COVIDHelp.Services;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
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
        public static IEnumerable<Position> Decode(string encodedPoints)
        {
            if (string.IsNullOrEmpty(encodedPoints))
                throw new ArgumentNullException("encodedPoints");

            char[] polylineChars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            while (index < polylineChars.Length)
            {
                // calculate next latitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                    break;

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                //calculate next longitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && next5bits >= 32)
                    break;

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                yield return new Position(Convert.ToDouble(currentLat) / 1E5, Convert.ToDouble(currentLng) / 1E5);

            }
        }
    }
}
