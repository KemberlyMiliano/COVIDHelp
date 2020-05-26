using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.Models
{
    public class User: INotifyPropertyChanged
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("phone")]
        public long Phone { get; set; }

        [JsonProperty("names")]
        public string Name { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("profilePhoto")]
        public string ProfilePhoto { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("selfBiography")]
        public string SelfBiography { get; set; }

        [JsonProperty("raters")]
        public double Raters { get; set; }

        [JsonProperty("stars")]
        public double Stars { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("helps")]
        public IList<Help> Helps { get; set; }

        [JsonProperty("activationCodes")]
        public ActivationCode ActivationCodes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
