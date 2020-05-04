using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
    public class ActivationCode
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("isCreated")]
        public bool IsCreated { get; set; }

        [JsonProperty("userID")]
        public int UserID { get; set; }
    }
}
