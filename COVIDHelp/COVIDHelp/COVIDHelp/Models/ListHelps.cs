using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
    public class ListHelps:List<Help>
    {
        public string Name { get; private set; }

        public ListHelps(string name, List<Help> helps) : base(helps)
        {
            Name = name;
        }
    }
}
