using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace COVIDHelp.Models
{
    public class Southwest
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
}
