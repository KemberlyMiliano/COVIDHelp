using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public class GoogleMapsApiService : IGoogleMapsApiService
    {
        public async Task<GooglePlaceAutoCompletePrediction> GetPlace(string text, string _googleMapsKey)
        {
            var getRequest = RestService.For<IGoogleMapsApiService>(ConfigApi.ApiKeyGoogle);
            var place = await getRequest.GetPlace(text,_googleMapsKey);
            return place;

        }
    }
}
