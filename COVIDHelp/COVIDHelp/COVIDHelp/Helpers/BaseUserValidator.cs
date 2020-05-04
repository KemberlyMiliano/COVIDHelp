using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace COVIDHelp.Helpers
{
    public static class BaseUserValidator
    {
        public static Regex Email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$");
        public static Regex Password = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");
        public static Regex Phone = new Regex(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
        public static Regex Name = new Regex(@"^[\p{L} \.'\-]+$");
    }
}
