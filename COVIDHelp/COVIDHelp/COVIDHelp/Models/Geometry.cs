using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
    public class Geometry
    {
        [JsonProperty("location")]
        public Locations Location { get; set; }

        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }
    }
}
