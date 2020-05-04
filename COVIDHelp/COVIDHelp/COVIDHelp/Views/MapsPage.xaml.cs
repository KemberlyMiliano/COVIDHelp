using COVIDHelp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using COVIDHelp.Helpers;
using COVIDHelp.Models;

namespace COVIDHelp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            InitializeComponent();
        }

        protected async override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            string latitude = null, longitude = null, type = null;
            latitude = latitude.GetPreferences("latitude");
            longitude = longitude.GetPreferences("longitude");
            type = type.GetPreferences("status");
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(latitude), double.Parse(longitude)), Distance.FromMiles(1)));
            if (BindingContext is MapsPageViewModel viewModel)
            {
                string place = type == $"{ETypeHelp.Alimentos}" ? "supermarket,grocery_or_supermarket" : "pharmacy";
                await viewModel.GetPlace($"{latitude},{longitude}",2000, place);
                var list = viewModel.PlaceNearbys;
                foreach (var item in list)
                {
                    map.Pins.Add(new Pin() {
                        Icon = SelectImage(type),
                    Position = item.Geometry.Location.Position,
                        Label = item.Name,
                        Address = item.Vicinity
                    });; ;
                }
              
            }
        }
        BitmapDescriptor SelectImage(string type) {
           var status =(ETypeHelp) Enum.Parse(typeof(ETypeHelp), type);
            switch (status)
            {
                case ETypeHelp.Medicamentos:
                    return BitmapDescriptorFactory.FromBundle("pharmacy_cross.png");
                case ETypeHelp.Alimentos:
                    return BitmapDescriptorFactory.FromBundle("comestibles.png");
                default:
                    return BitmapDescriptorFactory.DefaultMarker(Color.Blue);
            }
        }
    }
}