using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public interface IApiGoogleServices
    {   
        [Get("/api/place/nearbysearch/json?key={api_Key}&location={location}&radius={radius}&type={type}")]
        Task<NearbyPlaces> GetNearbyPlaces(string api_Key, string location,int radius,string type); 
        [Get("/api/directions/json?key={api_Key}&origin={origen}={destination}")]
        Task<NearbyPlaces> GetNearbyPlaces(string api_Key, string origen, string destination);
    }
}
