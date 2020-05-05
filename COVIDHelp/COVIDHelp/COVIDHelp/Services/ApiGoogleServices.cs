using COVIDHelp.Helpers;
using COVIDHelp.Models;
using MonkeyCache.FileStore;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace COVIDHelp.Services
{
    public class ApiGoogleServices : IApiGoogleServices
    {
        public ApiGoogleServices()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyCacheKey;
        }
        public async Task<NearbyPlaces> GetNearbyPlaces(string api_Key, string location, int radius, string type)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet&& Setting.MapsSetting)
            {
                return Barrel.Current.Get<NearbyPlaces>(key: $"{nameof(Setting.MapsSetting)}");
            }
            var getRequest = RestService.For<IApiGoogleServices>(ConfigApi.UrlApiGoogle);
            var places = await getRequest.GetNearbyPlaces(api_Key, location, radius, type);
            Barrel.Current.Add(key:$"{nameof(Setting.MapsSetting)}",places,expireIn:TimeSpan.FromDays(30));
            return places;
        }

    }
}
