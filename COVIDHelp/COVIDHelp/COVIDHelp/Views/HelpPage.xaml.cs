﻿using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace COVIDHelp.Views.HelpersViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
            string latitude = null, longitude = null;
            latitude = latitude.GetPreferences("latitude");
            longitude = longitude.GetPreferences("longitude");
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(latitude), double.Parse(longitude)), Distance.FromMiles(2)));
        }

        private void ListPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectHelp = e.CurrentSelection.FirstOrDefault() as Help;
            if (selectHelp!=null)
            {
                double lat = double.Parse(selectHelp.Latitude);
                double lng = double.Parse(selectHelp.Longitude);
                map.Pins.Add(new Pin
                {
                    Position = new Position(lat, lng),
                    Label = selectHelp.Needed.Name
                }) ;
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lng), Distance.FromMiles(2)));
            }
           
        }
    }
}