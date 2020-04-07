using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace COVIDHelp.Models
{
    public class Request : INotifyPropertyChanged
    {
        public string Text { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
