using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    interface IGoogleMapsApiService
    {
        [Get("api/place/autocomplete/json?input={Uri.EscapeUriString(text)}&key={_googleMapsKey}")]
        Task<GooglePlaceAutoCompletePrediction> GetPlace(string text,string _googleMapsKey);
    }
}
