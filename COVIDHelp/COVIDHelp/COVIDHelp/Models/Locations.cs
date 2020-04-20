using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.Models
{
    public class Locations
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        public Position Position { get => new Position(Lat, Lng); }
    }
}
