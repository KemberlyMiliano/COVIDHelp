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
            string type = null;
            string latitude = await Setting.Latitude();
            string longitude = await Setting.Longitude(); 
            type = type.GetPreferences("status");
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(latitude), double.Parse(longitude)), Distance.FromMiles(2)));
            if (BindingContext is MapsPageViewModel viewModel)
            {
                string place = type == $"{ETypeHelp.Alimentos}" ? "supermarket,grocery_or_supermarket" : "pharmacy";
                await viewModel.GetPlace($"{latitude},{longitude}",1000, place);
                var list = viewModel.PlaceNearbys;
                list.ForEach(e => map.Pins.Add(new Pin()
                {
                    Icon = SelectImage(type),
                    Position = e.Geometry.Location.Position,
                    Label = e.Name,
                    Address = e.Vicinity
                }));
              
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