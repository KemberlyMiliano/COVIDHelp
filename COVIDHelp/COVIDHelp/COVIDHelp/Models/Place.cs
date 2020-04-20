using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
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
}
