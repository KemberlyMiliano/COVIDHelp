using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
    public class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }
        public string OpenOrClose { get => OpenNow == true ? "Abierto" : "Cerrado"; }

    }
}
