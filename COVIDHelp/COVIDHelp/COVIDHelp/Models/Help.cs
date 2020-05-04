using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
    public class Help
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("userID")]
        public int UserID { get; set; }
        [JsonProperty("needed")]
        public User Needed { get; set; }

        [JsonProperty("volunteer")]
        public User Volunteer { get; set; }

        [JsonProperty("volunteerID")]
        public int VolunteerID { get; set; }

        [JsonIgnore]
        public double State
        {
            get
            {
                var type = Enum.Parse(typeof(EState), Status);
                switch (type)
                {
                    case EState.Activo:
                        {
                            return 0;
                        }
                    case EState.Proceso:
                        {
                            return 0.5;

                        }
                    case EState.Completado:
                        {
                            return 1;

                        }
                    default:

                        return 0;
                }
            }
        }
    }
}
