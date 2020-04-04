using COVIDHelp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.ViewModels
{
    public class MapsPageViewModel:BaseViewModel
    {
        public ObservableCollection<Location> FoodsPin { get; set; }
        public ObservableCollection<Location> PharmacyPin { get; set; }
        public DelegateCommand LoadPins { get; set; }
        Geocoder geoCoder = new Geocoder();
        public MapsPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {

            LoadPins = new DelegateCommand(async () =>
            {
                FoodsPin = new ObservableCollection<Location>();
                PharmacyPin = new ObservableCollection<Location>();
            });
            LoadPins.Execute();

        }
        async Task<Position> GetPosition()
        {
            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync("New York, USA");
            Position position = approximateLocations.FirstOrDefault();
            return new Position(position.Latitude, position.Longitude);
        }
    }
}
