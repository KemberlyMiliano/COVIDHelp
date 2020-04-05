using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.Models
{
    public class Location:INotifyPropertyChanged
    {
        private Position position;

        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        public string Address { get; set; }
        public string Description { get; set; }
        public Location(string address, string description, Position position)
        {
            Address = address;
            Description = description;
            Position = position;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
