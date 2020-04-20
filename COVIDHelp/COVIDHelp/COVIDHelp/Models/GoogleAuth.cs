using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
    public class GoogleAuth
    {
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}
