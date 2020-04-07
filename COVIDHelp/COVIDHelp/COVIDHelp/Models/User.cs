using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cedula")]
        public Int64 Cedula { get; set; }

        [JsonProperty("nombres")]
        public string Nombres { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }

        [JsonProperty("correo")]
        public string Correo { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("rol")]
        public string Rol { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("fNacimiento")]
        public DateTime FNacimiento { get; set; }

        [JsonProperty("foto")]
        public string Foto { get; set; }

        [JsonProperty("sexo")]
        public string Sexo { get; set; }

        [JsonProperty("selfBiography")]
        public string SelfBiography { get; set; }

        [JsonProperty("raters")]
        public int Raters { get; set; }

        [JsonProperty("stars")]
        public int Stars { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }
    }

}
