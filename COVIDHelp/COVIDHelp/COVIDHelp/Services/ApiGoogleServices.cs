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
            Barrel.ApplicationId = ConfigApi.MonkeyChadeKey;
        }
        public async Task<NearbyPlaces> GetNearbyPlaces(string api_Key, string location, int radius, string type)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet&& !Barrel.Current.Exists(key:$"{nameof(GetNearbyPlaces)}"))
            {
                return Barrel.Current.Get<NearbyPlaces>(key: $"{nameof(GetNearbyPlaces)}");
            }
            var getRequest = RestService.For<IApiGoogleServices>(ConfigApi.UrlApiGoogle);
            var places = await getRequest.GetNearbyPlaces(api_Key, location, radius, type);
            Barrel.Current.Add(key:$"{nameof(GetNearbyPlaces)}",places,expireIn:TimeSpan.FromDays(1));
            return places;
        }

    }
}
