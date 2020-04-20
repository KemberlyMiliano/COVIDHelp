using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.Models
{
    public class NearbyPlaces
    {
        [JsonProperty("results")]
        public IList<Place> Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
