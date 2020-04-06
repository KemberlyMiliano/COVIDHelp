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
using COVIDHelp.Views.TemplateViews;

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
            type = type.GetPreferences("type");

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(latitude), double.Parse(longitude)), Distance.FromMiles(100)));
            if (BindingContext is MapsPageViewModel viewModel)
            {
                await viewModel.GetPlace($"{latitude},{longitude}",20000,type);
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
            switch (type)
            {
                case "pharmacy":
                    return BitmapDescriptorFactory.FromBundle("pharmacy_cross.png");
                case "supermarket":
                    return BitmapDescriptorFactory.FromBundle("comestibles.png");
                default:
                    return BitmapDescriptorFactory.DefaultMarker(Color.Blue);
            }
        }
    }
}