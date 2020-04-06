using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.Models
{
    public class Locations
    {

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        public Position Position { get=>new Position(Lat,Lng); }
    }

    public class Northeast
    {

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public class Southwest
    {

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public class Viewport
    {

        [JsonProperty("northeast")]
        public Northeast Northeast { get; set; }

        [JsonProperty("southwest")]
        public Southwest Southwest { get; set; }
    }

    public class Geometry
    {

        [JsonProperty("location")]
        public Locations Location { get; set; }

        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }
    }

    public class OpeningHours
    {

        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }
    }

    public class Photo
    {

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("html_attributions")]
        public IList<string> HtmlAttributions { get; set; }

        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public class PlusCode
    {

        [JsonProperty("compound_code")]
        public string CompoundCode { get; set; }

        [JsonProperty("global_code")]
        public string GlobalCode { get; set; }
    }

    public class Place
    {

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("icon")]
        private string icon;

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }


        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("opening_hours")]
        public OpeningHours OpeningHours { get; set; }

        [JsonProperty("photos")]
        public IList<Photo> Photos { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("types")]
        public IList<string> Types { get; set; }

        [JsonProperty("user_ratings_total")]
        public int UserRatingsTotal { get; set; }
        public int Radius { get; set; }
        public string TypePlace { get; set; }
        
         [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

    }

    public class NearbyPlaces
    {


        [JsonProperty("results")]
        public IList<Place> Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
