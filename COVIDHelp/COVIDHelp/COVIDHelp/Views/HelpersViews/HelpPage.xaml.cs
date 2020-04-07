using COVIDHelp.Helpers;
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

        }
        protected async override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            string latitude = null, longitude = null, type = null;
            latitude = latitude.GetPreferences("latitude");
            longitude = longitude.GetPreferences("longitude");
            type = type.GetPreferences("type");
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(latitude), double.Parse(longitude)), Distance.FromMiles(2)));
            if (BindingContext is HelpPageViewModel viewModel)
            {             
                await viewModel.GetPerson();
                foreach (var item in viewModel.HelpsPerson)
                {

                    double lat = double.Parse(item.Posicion.Split(',')[0]);
                     double lng = double.Parse(item.Posicion.Split(',')[1]);
                    map.Pins.Add(new Pin()
                    {
                        Position = new Position(lat, lng),
                        Label = item.Nombre
                    });
                }

            }
        

        }
    }
}